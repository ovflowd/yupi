namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomErrorMessageComposer : Yupi.Messages.Contracts.RoomErrorMessageComposer
    {
        #region Methods

        // TODO ErrorCode???
        public override void Compose(Yupi.Protocol.ISender session, int errorCode)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(errorCode);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}