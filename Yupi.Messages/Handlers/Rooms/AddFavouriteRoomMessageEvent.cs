using System;
using Yupi.Messages.Rooms;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Rooms
{
	public class AddFavouriteRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();
			router.GetComposer<FavouriteRoomsUpdateMessageComposer> ().Compose (session, roomId, true);

			session.GetHabbo().FavoriteRooms.Add(roomId);
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager ().GetQueryReactor ()) {
				queryReactor.SetQuery ("INSERT INTO users_favorites (user_id,room_id) VALUES (@user, @room)");
				queryReactor.AddParameter ("user", session.GetHabbo ().Id);
				queryReactor.AddParameter ("room", roomId);
				queryReactor.RunQuery ();
			}
		}
	}
}

