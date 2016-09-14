namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class FindMoreFriendsSuccessMessageComposer : Yupi.Messages.Contracts.FindMoreFriendsSuccessMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool success)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(success);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}