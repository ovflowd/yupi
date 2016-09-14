namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomUserIdleMessageComposer : Yupi.Messages.Contracts.RoomUserIdleMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int entityId, bool isAsleep)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendBool(isAsleep);
                room.Send(message);
            }
        }

        #endregion Methods
    }
}