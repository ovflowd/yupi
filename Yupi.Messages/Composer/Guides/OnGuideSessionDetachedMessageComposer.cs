namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Protocol.Buffers;

    public class OnGuideSessionDetachedMessageComposer : Yupi.Messages.Contracts.OnGuideSessionDetachedMessageComposer
    {
        #region Methods

        // TODO Meaning of value (enum)
        public override void Compose(Yupi.Protocol.ISender session, int value)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(value);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}