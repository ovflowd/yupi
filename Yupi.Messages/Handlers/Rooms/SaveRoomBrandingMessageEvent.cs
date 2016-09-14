using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class SaveRoomBrandingMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var itemId = request.GetUInt32();
            var count = request.GetUInt32();
            /*
            if (session?.GetHabbo() == null)
                return;
            Room room = session.GetHabbo().CurrentRoom;

            if (room == null || !room.CheckRights(session, true))
                return;

            RoomItem item = room.GetRoomItemHandler().GetItem(itemId);

            if (item == null)
                return;

            string extraData = "state"+Convert.ToChar(9)+"0";

            for (uint i = 1; i <= count; i++)
                extraData = extraData + Convert.ToChar(9) + request.GetString();

            item.ExtraData = extraData;

            room.GetRoomItemHandler().SetFloorItem(session, item, item.X, item.Y, item.Rot, false, false, true);
*/
            throw new NotImplementedException();
        }
    }
}