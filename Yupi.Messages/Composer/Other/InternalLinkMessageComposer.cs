namespace Yupi.Messages.Other
{
    using System;

    using Yupi.Protocol.Buffers;

    public class InternalLinkMessageComposer : Yupi.Messages.Contracts.InternalLinkMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string link)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(link);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}