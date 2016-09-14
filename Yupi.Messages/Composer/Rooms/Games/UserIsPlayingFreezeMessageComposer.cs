namespace Yupi.Messages.Games
{
    using System;

    using Yupi.Protocol.Buffers;

    public class UserIsPlayingFreezeMessageComposer : Yupi.Messages.Contracts.UserIsPlayingFreezeMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool isPlaying)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(isPlaying);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}