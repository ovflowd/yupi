namespace Yupi.Messages.GameCenter
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GameCenterGamesLeftMessageComposer : Yupi.Messages.Contracts.GameCenterGamesLeftMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // TODO  hardcoded message
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(18);
                message.AppendInteger(-1);
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}