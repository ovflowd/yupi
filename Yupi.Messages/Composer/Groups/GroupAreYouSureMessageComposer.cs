namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GroupAreYouSureMessageComposer : Yupi.Messages.Contracts.GroupAreYouSureMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int userId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(userId);
                message.AppendInteger(0); // TODO Hardcoded
                session.Send(message);
            }
        }

        #endregion Methods
    }
}