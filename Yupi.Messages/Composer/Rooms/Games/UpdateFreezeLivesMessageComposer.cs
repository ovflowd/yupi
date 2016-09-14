namespace Yupi.Messages.Games
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UpdateFreezeLivesMessageComposer : Yupi.Messages.Contracts.UpdateFreezeLivesMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, int lives)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(lives);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}