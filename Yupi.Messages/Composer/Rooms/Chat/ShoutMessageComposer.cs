namespace Yupi.Messages.Chat
{
    using System;

    using Yupi.Messages.Encoders;
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ShoutMessageComposer : Yupi.Messages.Contracts.ShoutMessageComposer
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