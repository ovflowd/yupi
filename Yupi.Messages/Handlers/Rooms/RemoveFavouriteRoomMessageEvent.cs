namespace Yupi.Messages.Rooms
{
    using System;

    public class RemoveFavouriteRoomMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint roomId = request.GetUInt32();

            /*
            session.GetHabbo().FavoriteRooms.Remove(roomId);
            router.GetComposer<FavouriteRoomsUpdateMessageComposer> ().Compose (session, roomId, false);

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) {
                queryReactor.SetQuery ("DELETE FROM users_favorites WHERE user_id = @user AND room_id = @room");
                queryReactor.AddParameter ("user", session.GetHabbo ().Id);
                queryReactor.AddParameter ("room", roomId);
                queryReactor.RunQuery ();
            }
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}