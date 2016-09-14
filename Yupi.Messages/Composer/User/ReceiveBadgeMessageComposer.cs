namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ReceiveBadgeMessageComposer : Yupi.Messages.Contracts.ReceiveBadgeMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string badgeId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendString(badgeId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}