namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Protocol.Buffers;

    public class MinimailCountMessageComposer : Yupi.Messages.Contracts.MinimailCountMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int count)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(count);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}