namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomEnterErrorMessageComposer : Yupi.Messages.Contracts.RoomEnterErrorMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Error error)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) error);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}