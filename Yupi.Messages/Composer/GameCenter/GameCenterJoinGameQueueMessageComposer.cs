namespace Yupi.Messages.GameCenter
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GameCenterJoinGameQueueMessageComposer : Yupi.Messages.Contracts.GameCenterJoinGameQueueMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO  hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}