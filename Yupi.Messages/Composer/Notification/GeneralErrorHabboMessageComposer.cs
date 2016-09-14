namespace Yupi.Messages.Notification
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GeneralErrorHabboMessageComposer : Yupi.Messages.Contracts.GeneralErrorHabboMessageComposer
    {
        #region Methods

        // TODO Replace errorId with enum
        public override void Compose(Yupi.Protocol.ISender session, int errorId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(errorId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}