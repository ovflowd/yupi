namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Protocol.Buffers;

    public class NotAcceptingRequestsMessageComposer : Contracts.NotAcceptingRequestsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(39);
                message.AppendInteger(3);
                session.Send(message); // TODO Hardcoded
            }
        }

        #endregion Methods
    }
}