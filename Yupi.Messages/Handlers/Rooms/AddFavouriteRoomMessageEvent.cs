using System;
using Yupi.Messages.Rooms;


namespace Yupi.Messages.Rooms
{
	public class AddFavouriteRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
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

