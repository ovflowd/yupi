namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class DoorbellOpenedMessageComposer : Yupi.Messages.Contracts.DoorbellOpenedMessageComposer
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