using System;

using System.Collections.Generic;


namespace Yupi.Messages.Rooms
{
	public class RoomSaveSettingsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

			if (room == null || !room.CheckRights(session, true))
				return;

			// TODO Unused
			request.GetInteger();

			string oldName = room.RoomData.Name;

			room.RoomData.Name = request.GetString();

			if (room.RoomData.Name.Length < 3)
			{
				room.RoomData.Name = oldName;
				return;
			}

			room.RoomData.Description = request.GetString();
			room.RoomData.State = request.GetInteger();

			if (room.RoomData.State < 0 || room.RoomData.State > 2)
			{
				room.RoomData.State = 0;

				return;
			}
			room.RoomData.PassWord = request.GetString();
			room.RoomData.UsersMax = request.GetUInt32();
			room.RoomData.Category = request.GetInteger();

			uint tagCount = request.GetUInt32();

			if (tagCount > 2)
				return;

			List<string> tags = new List<string>();

			for (int i = 0; i < tagCount; i++)
				tags.Add(request.GetString().ToLower());

			room.RoomData.TradeState = request.GetInteger();
			room.RoomData.AllowPets = request.GetBool();
			room.RoomData.AllowPetsEating = request.GetBool();
			room.RoomData.AllowWalkThrough = request.GetBool();
			room.RoomData.HideWall = request.GetBool();
			room.RoomData.WallThickness = request.GetInteger();

			if (room.RoomData.WallThickness < -2 || room.RoomData.WallThickness > 1)
				room.RoomData.WallThickness = 0;

			room.RoomData.FloorThickness = request.GetInteger();

			if (room.RoomData.FloorThickness < -2 || room.RoomData.FloorThickness > 1)
				room.RoomData.FloorThickness = 0;

			room.RoomData.WhoCanMute = request.GetInteger();
			room.RoomData.WhoCanKick = request.GetInteger();
			room.RoomData.WhoCanBan = request.GetInteger();
			room.RoomData.ChatType = request.GetInteger();
			room.RoomData.ChatBalloon = request.GetUInt32();
			room.RoomData.ChatSpeed = request.GetUInt32();
			room.RoomData.ChatMaxDistance = request.GetUInt32();

			// TODO Check this in setter!
			if (room.RoomData.ChatMaxDistance > 90)
				room.RoomData.ChatMaxDistance = 90;

			room.RoomData.ChatFloodProtection = request.GetUInt32(); //chat_flood_sensitivity

			if (room.RoomData.ChatFloodProtection > 2)
				room.RoomData.ChatFloodProtection = 2;

			request.GetBool(); //allow_dyncats_checkbox

			PublicCategory flatCat = Yupi.GetGame().GetNavigator().GetFlatCat(room.RoomData.Category);

			if (flatCat == null || flatCat.MinRank > session.GetHabbo().Rank)
				room.RoomData.Category = 0;

			room.ClearTags();
			room.AddTagRange(tags);

			router.GetComposer<RoomSettingsSavedMessageComposer> ().Compose (session, room.RoomId);
			router.GetComposer<RoomUpdateMessageComposer> ().Compose (session, room.RoomId);
			router.GetComposer<RoomFloorWallLevelsMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom, room.RoomData);
			router.GetComposer<RoomChatOptionsMessageComposer> ().Compose (session.GetHabbo ().CurrentRoom, room.RoomData);

			router.GetComposer<RoomDataMessageComposer> ().Compose (room, room, true, true);
		}
	}
}

