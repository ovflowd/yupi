namespace Yupi.Messages.User
{
    using System;

    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public class CitizenshipStatusMessageComposer : Yupi.Messages.Contracts.CitizenshipStatusMessageComposer
    {
        #region Methods

        // TODO Replace value with a proper name
        public override void Compose(Yupi.Protocol.ISender session, string value)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(value);
                message.AppendInteger(4);
                message.AppendInteger(4); // TODO magic constant
                session.Send(message);
            }
        }

        #endregion Methods
    }
}