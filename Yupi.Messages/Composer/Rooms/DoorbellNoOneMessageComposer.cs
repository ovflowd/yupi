namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class DoorbellNoOneMessageComposer : Yupi.Messages.Contracts.DoorbellNoOneMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(string.Empty); // TODO What can this be used for?
                session.Send(message);
            }
        }

        #endregion Methods
    }
}