namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class RoomRightsLevelMessageComposer : Yupi.Messages.Contracts.RoomRightsLevelMessageComposer
    {
        #region Methods

        // TODO Level should be enum
        public override void Compose(Yupi.Protocol.ISender session, int level)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(level);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}