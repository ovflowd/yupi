namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomForwardMessageComposer : Yupi.Messages.Contracts.RoomForwardMessageComposer
    {
        #region Methods

        // TODO Use RoomInfo
        public override void Compose(Yupi.Protocol.ISender session, int roomId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}