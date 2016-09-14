namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ConsoleChatErrorMessageComposer : Yupi.Messages.Contracts.ConsoleChatErrorMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int errorId, uint conversationId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(errorId);
                message.AppendInteger(conversationId);
                message.AppendString(string.Empty);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}