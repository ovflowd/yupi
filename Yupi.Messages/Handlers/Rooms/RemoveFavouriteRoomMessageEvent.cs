using System;


namespace Yupi.Messages.Rooms
{
	public class RemoveFavouriteRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint roomId = request.GetUInt32();

			session.GetHabbo().FavoriteRooms.Remove(roomId);
			router.GetComposer<FavouriteRoomsUpdateMessageComposer> ().Compose (session, roomId, false);

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
				queryReactor.SetQuery ("DELETE FROM users_favorites WHERE user_id = @user AND room_id = @room");
				queryReactor.AddParameter ("user", session.GetHabbo ().Id);
				queryReactor.AddParameter ("room", roomId);
				queryReactor.RunQuery ();
			}
		}
	}
}

