namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class DoorbellMessageComposer : Yupi.Messages.Contracts.DoorbellMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string username)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(username);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}