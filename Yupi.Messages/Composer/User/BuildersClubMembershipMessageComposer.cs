namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class BuildersClubMembershipMessageComposer : Yupi.Messages.Contracts.BuildersClubMembershipMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int expire, int maxItems)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(expire);
                message.AppendInteger(maxItems);
                message.AppendInteger(2); // TODO Hardcoded
                session.Send(message);
            }
        }

        #endregion Methods
    }
}