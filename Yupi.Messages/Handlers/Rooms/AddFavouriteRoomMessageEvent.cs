using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class AddFavouriteRoomMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetInteger();
            router.GetComposer<FavouriteRoomsUpdateMessageComposer>().Compose(session, roomId, true);
            throw new NotImplementedException();
            //session.Info.FavoriteRooms.Add(roomId);
        }
    }
}