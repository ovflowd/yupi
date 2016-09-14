namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomMuteStatusMessageComposer : Yupi.Messages.Contracts.RoomMuteStatusMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool isMuted)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(isMuted);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}