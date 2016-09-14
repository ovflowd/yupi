namespace Yupi.Messages.Wired
{
    using System;

    using Yupi.Protocol.Buffers;

    public class WiredRewardAlertMessageComposer : Yupi.Messages.Contracts.WiredRewardAlertMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int status)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(status); // TODO Use enum
                session.Send(message);
            }
        }

        #endregion Methods
    }
}