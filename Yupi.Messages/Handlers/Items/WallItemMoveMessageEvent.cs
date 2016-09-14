namespace Yupi.Messages.Items
{
    using System;

    public class WallItemMoveMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session))
                return;

            uint id = request.GetUInt32();
            string locationData = request.GetString();

            RoomItem item = room.GetRoomItemHandler().GetItem(id);

            if (item == null)
                return;

            try
            {
                WallCoordinate wallCoord = new WallCoordinate(":" + locationData.Split(':')[1]);
                item.WallCoord = wallCoord;
            }
            catch
            {
                // TODO Silent catch
                return;
            }

            room.GetRoomItemHandler().AddOrUpdateItem(id);

            router.GetComposer<UpdateRoomWallItemMessageComposer> ().Compose (room, item);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}