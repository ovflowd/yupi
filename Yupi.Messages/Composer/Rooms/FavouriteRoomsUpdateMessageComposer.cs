namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class FavouriteRoomsUpdateMessageComposer : Yupi.Messages.Contracts.FavouriteRoomsUpdateMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, bool isAdded)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(roomId);
                message.AppendBool(isAdded);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}