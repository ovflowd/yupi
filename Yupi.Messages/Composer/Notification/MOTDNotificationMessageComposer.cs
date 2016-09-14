namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Protocol.Buffers;

    public class MOTDNotificationMessageComposer : Yupi.Messages.Contracts.MOTDNotificationMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string text)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendString(text);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}