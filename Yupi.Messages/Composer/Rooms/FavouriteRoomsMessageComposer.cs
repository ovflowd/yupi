using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class FavouriteRoomsMessageComposer : Contracts.FavouriteRoomsMessageComposer
    {
        public override void Compose(ISender session, IList<RoomData> rooms)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(30); // TODO Hardcoded value
                message.AppendInteger(rooms.Count);

                foreach (var room in rooms)
                    message.AppendInteger(room.Id);
                session.Send(message);
            }
        }
    }
}