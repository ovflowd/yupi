using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
    public class RoomAddPostItMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if ((session.Room == null) || !session.Room.Data.HasRights(session.Info))
                return;

            var id = request.GetInteger();
            var locationData = request.GetString();

            /*
            UserItem item = session.GetHabbo ().GetInventoryComponent ().GetItem (id);

            if (item == null)
                return;

            WallCoordinate wallCoord = new WallCoordinate (":" + locationData.Split (':') [1]);

            RoomItem item2 = new RoomItem (item.Id, room.RoomId, item.BaseItem.Name, item.ExtraData, wallCoord, room,
                                  session.GetHabbo ().Id, item.GroupId, false);

            if (room.GetRoomItemHandler ().SetWallItem (session, item2))
                session.GetHabbo ().GetInventoryComponent ().RemoveItem (id, true);
*/
            throw new NotImplementedException();
        }
    }
}