namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class SuggestPollMessageComposer : Yupi.Messages.Contracts.SuggestPollMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Poll poll)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(Id);
                message.AppendString(string.Empty); //?
                message.AppendString(poll.Invitation);
                message.AppendString("Test"); // whats this??
                session.Send(message);
            }
        }

        #endregion Methods
    }
}