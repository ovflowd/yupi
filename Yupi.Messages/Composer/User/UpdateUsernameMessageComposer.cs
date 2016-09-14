namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UpdateUsernameMessageComposer : Yupi.Messages.Contracts.UpdateUsernameMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string newName)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(0); // TODO Magic constant
                message.AppendString(newName);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}