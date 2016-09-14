namespace Yupi.Messages.Items
{
    using System;

    public class OpenPostItMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);
            RoomItem item = room?.GetRoomItemHandler().GetItem(request.GetUInt32());

            router.GetComposer<LoadPostItMessageComposer> ().Compose (session, item);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}