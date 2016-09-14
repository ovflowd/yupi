namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class HomeRoomMessageComposer : Yupi.Messages.Contracts.HomeRoomMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendInteger(0); // TODO Hardcoded
                session.Send(message);
            }
        }

        #endregion Methods
    }
}