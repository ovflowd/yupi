using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.Browser.Enums;
using Yupi.Messages.Rooms;

namespace Yupi.Messages.Navigator
{
	public class ToggleStaffPickMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			request.GetBool(); // TODO Unused

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_Spr", 1, true);

			if (room == null)
				return;

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				PublicItem pubItem = Yupi.GetGame().GetNavigator().GetPublicRoom(roomId);

				if (pubItem == null) // Isn't A Staff Pick Room
				{
					queryReactor.SetQuery("INSERT INTO navigator_publics (bannertype, room_id, category_parent_id) VALUES ('0', @roomId, '-2')");

					queryReactor.AddParameter("roomId", room.RoomId);

					uint lastInsertId = (uint) queryReactor.InsertQuery();

					PublicItem publicItem = new PublicItem(lastInsertId, 0, string.Empty, string.Empty, string.Empty, PublicImageType.Internal, room.RoomId, 0, -2, false, 1);

					Yupi.GetGame().GetNavigator().AddPublicRoom(publicItem);
				}
				else // Is a Staff Pick Room
				{
					queryReactor.SetQuery("DELETE FROM navigator_publics WHERE id = @pubId");

					queryReactor.AddParameter("pubId", pubItem.Id);
					queryReactor.RunQuery();

					Yupi.GetGame().GetNavigator().RemovePublicRoom(pubItem.Id);
				}

				router.GetComposer<RoomDataMessageComposer> ().Compose (room, room, true, true);
			}
		}
	}
}

