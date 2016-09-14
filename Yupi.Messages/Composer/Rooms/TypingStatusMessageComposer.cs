namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class TypingStatusMessageComposer : Yupi.Messages.Contracts.TypingStatusMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int virtualId, bool isTyping)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(isTyping);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}