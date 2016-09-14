namespace Yupi.Messages.GameCenter
{
    using System;

    using Yupi.Protocol.Buffers;

    public class GameCenterGamesListMessageComposer : Yupi.Messages.Contracts.GameCenterGamesListMessageComposer
    {
        #region Methods

        // TODO Hardcoded message
        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(18);
                message.AppendString("elisa_habbo_stories");
                message.AppendString("000000");
                message.AppendString("ffffff");
                message.AppendString("");
                message.AppendString("");

                session.Send(message);
            }
        }

        #endregion Methods
    }
}