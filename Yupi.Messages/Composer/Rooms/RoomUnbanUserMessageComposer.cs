namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomUnbanUserMessageComposer : Yupi.Messages.Contracts.RoomUnbanUserMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint roomId, uint userId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(userId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}