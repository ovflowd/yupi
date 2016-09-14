using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RemoveFavouriteRoomMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetUInt32();

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
    }
}