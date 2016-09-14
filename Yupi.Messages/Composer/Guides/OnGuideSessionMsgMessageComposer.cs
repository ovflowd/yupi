namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Protocol.Buffers;

    // TODO Rename
    public class OnGuideSessionMsgMessageComposer : Yupi.Messages.Contracts.OnGuideSessionMsgMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string content, int userId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(content);
                message.AppendInteger(userId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}