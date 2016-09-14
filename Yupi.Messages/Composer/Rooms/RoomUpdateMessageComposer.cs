namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomUpdateMessageComposer : Yupi.Messages.Contracts.RoomUpdateMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}