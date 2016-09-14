namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class OutOfRoomMessageComposer : Yupi.Messages.Contracts.OutOfRoomMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, short code = 0)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendShort(code); // TODO Also possible without code & what does code mean.
                session.Send(message);
            }
        }

        #endregion Methods
    }
}