namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Messages.Rooms;

    public class AddFavouriteRoomMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int roomId = request.GetInteger();
            router.GetComposer<FavouriteRoomsUpdateMessageComposer>().Compose(session, roomId, true);
            throw new NotImplementedException();
            //session.Info.FavoriteRooms.Add(roomId);
        }

        #endregion Methods
    }
}