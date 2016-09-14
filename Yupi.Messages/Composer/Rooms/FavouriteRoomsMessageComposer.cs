namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class FavouriteRoomsMessageComposer : Yupi.Messages.Contracts.FavouriteRoomsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<RoomData> rooms)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(30); // TODO Hardcoded value
                message.AppendInteger(rooms.Count);

                foreach (RoomData room in rooms)
                    message.AppendInteger(room.Id);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}