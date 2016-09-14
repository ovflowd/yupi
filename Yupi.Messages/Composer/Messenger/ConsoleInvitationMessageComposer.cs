namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ConsoleInvitationMessageComposer : Yupi.Messages.Contracts.ConsoleInvitationMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int habboId, string content)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(habboId);
                message.AppendString(content);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}