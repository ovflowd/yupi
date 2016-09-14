namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UserClubRightsMessageComposer : Yupi.Messages.Contracts.UserClubRightsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool hasVIP, int rank, bool isAmbadassor = false)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(hasVIP); // TODO Is enum
                message.AppendInteger(rank);
                message.AppendBool(isAmbadassor);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}