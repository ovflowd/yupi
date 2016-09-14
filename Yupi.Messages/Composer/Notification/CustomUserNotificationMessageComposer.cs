namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Protocol.Buffers;

    public class CustomUserNotificationMessageComposer : Yupi.Messages.Contracts.CustomUserNotificationMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(3);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}