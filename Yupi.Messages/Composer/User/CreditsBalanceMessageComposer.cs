namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class CreditsBalanceMessageComposer : Contracts.CreditsBalanceMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int credits)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(credits.ToString());
                session.Send(message);
            }
        }

        #endregion Methods
    }
}