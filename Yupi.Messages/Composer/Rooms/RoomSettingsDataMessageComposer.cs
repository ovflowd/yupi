using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomSettingsDataMessageComposer : AbstractComposer<Room>
	{
		public override void Compose (Yupi.Protocol.ISender session, Room room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.RoomId);
				message.AppendString(room.RoomData.Name);
				message.AppendString(room.RoomData.Description);
				message.AppendInteger(room.RoomData.State);
				message.AppendInteger(room.RoomData.Category);
				message.AppendInteger(room.RoomData.UsersMax);
				message.AppendInteger(room.RoomData.Model.MapSizeX*room.RoomData.Model.MapSizeY > 200 ? 50 : 25); // TODO Magic number
				message.AppendInteger(room.TagCount);

				foreach (string s in room.RoomData.Tags)
					message.AppendString(s);

				message.AppendInteger(room.RoomData.TradeState);
				message.AppendInteger(room.RoomData.AllowPets);
				message.AppendInteger(room.RoomData.AllowPetsEating);
				message.AppendInteger(room.RoomData.AllowWalkThrough);
				message.AppendInteger(room.RoomData.HideWall);
				message.AppendInteger(room.RoomData.WallThickness);
				message.AppendInteger(room.RoomData.FloorThickness);
				message.AppendInteger(room.RoomData.ChatType);
				message.AppendInteger(room.RoomData.ChatBalloon);
				message.AppendInteger(room.RoomData.ChatSpeed);
				message.AppendInteger(room.RoomData.ChatMaxDistance);
				message.AppendInteger(room.RoomData.ChatFloodProtection > 2 ? 2 : room.RoomData.ChatFloodProtection);
				message.AppendBool(false); //allow_dyncats_checkbox
				message.AppendInteger(room.RoomData.WhoCanMute);
				message.AppendInteger(room.RoomData.WhoCanKick);
				message.AppendInteger(room.RoomData.WhoCanBan);
				session.Send (message);
			}
		}
	}
}

