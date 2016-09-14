namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Protocol.Buffers;

    public class OnGuideSessionInvitedToGuideRoomMessageComposer : Yupi.Messages.Contracts.OnGuideSessionInvitedToGuideRoomMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, string roomName)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendString(roomName);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}