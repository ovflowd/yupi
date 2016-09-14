namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class EnableNotificationsMessageComposer : Yupi.Messages.Contracts.EnableNotificationsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true); //isOpen
                message.AppendBool(false);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}