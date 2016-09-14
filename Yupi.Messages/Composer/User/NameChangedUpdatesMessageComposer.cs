namespace Yupi.Messages.User
{
    using System;
    using System.Collections.Generic;

    using Yupi.Messages.Encoders;
    using Yupi.Protocol.Buffers;

    // TODO Shouldn't this be called NameCHECKED
    public class NameChangedUpdatesMessageComposer : Yupi.Messages.Contracts.NameChangedUpdatesMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Status status, string newName,
            IList<string> alternatives = null)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) status);
                message.AppendString(newName);

                if (alternatives == null)
                {
                    message.AppendInteger(0); // TODO Magic constant
                }
                else
                {
                    message.Append(alternatives);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}