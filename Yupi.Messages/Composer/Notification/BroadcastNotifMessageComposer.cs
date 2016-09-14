namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Protocol.Buffers;

    public class BroadcastNotifMessageComposer : Yupi.Messages.Contracts.BroadcastNotifMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string text)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(text);
                message.AppendString(string.Empty);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}