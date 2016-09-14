namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Protocol.Buffers;

    public class FollowFriendErrorMessageComposer : Yupi.Messages.Contracts.FollowFriendErrorMessageComposer
    {
        #region Methods

        // TODO Enum
        public override void Compose(Yupi.Protocol.ISender session, int status)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(status);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}