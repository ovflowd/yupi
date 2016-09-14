namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomUnbanUserMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            uint userId = request.GetUInt32();
            uint roomId = request.GetUInt32();

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null)
                return;

            room.Unban(userId);

            router.GetComposer<RoomUnbanUserMessageComposer> ().Compose (session, roomId, userId);
            */
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}