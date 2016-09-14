namespace Yupi.Messages.Chat
{
    using System;

    using Yupi.Controller;
    using Yupi.Messages.Encoders;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;
    using Yupi.Util;

    public class ChatMessageComposer : Yupi.Messages.Contracts.ChatMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, ChatMessage msg, int count = -1)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.Append(msg, count);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}