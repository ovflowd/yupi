namespace Yupi.Messages.Items
{
    using System;

    public class TradeAcceptMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(session.GetHabbo().Id);

            userTrade?.Accept(session.GetHabbo().Id);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}