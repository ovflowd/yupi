namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class ApplyHanditemMessageComposer : Yupi.Messages.Contracts.ApplyHanditemMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int virtualId, int itemId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}