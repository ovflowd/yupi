using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class RemovePostItMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(request.GetUInt32());

            if (item == null || item.GetBaseItem().InteractionType != Interaction.PostIt)
                return;

            room.GetRoomItemHandler().RemoveFurniture(session, item.Id);
            */
            throw new NotImplementedException();
        }
    }
}