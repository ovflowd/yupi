using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class TradeRemoveItemMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CanTradeInRoom)
                return;

            Trade userTrade = room.GetUserTrade(session.GetHabbo().Id);
            UserItem item = session.GetHabbo().GetInventoryComponent().GetItem(request.GetUInt32());

            if (userTrade == null || item == null)
                return;

            userTrade.TakeBackItem(session.GetHabbo().Id, item);
            */
            throw new NotImplementedException();
        }
    }
}