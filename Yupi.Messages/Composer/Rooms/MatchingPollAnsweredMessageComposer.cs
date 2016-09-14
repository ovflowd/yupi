namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class MatchingPollAnsweredMessageComposer : Yupi.Messages.Contracts.MatchingPollAnsweredMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint userId, string text)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                message.AppendString(text);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}