namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UpdateIgnoreStatusMessageComposer : Yupi.Messages.Contracts.UpdateIgnoreStatusMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, State state, string username)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) state);
                message.AppendString(username);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}