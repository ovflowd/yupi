namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Protocol.Buffers;

    public class OnGuideSessionErrorComposer : Yupi.Messages.Contracts.OnGuideSessionErrorComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO Hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}