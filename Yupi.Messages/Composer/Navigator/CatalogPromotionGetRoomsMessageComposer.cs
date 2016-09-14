using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Navigator
{
    public class CatalogPromotionGetRoomsMessageComposer :
        Yupi.Messages.Contracts.CatalogPromotionGetRoomsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<RoomData> rooms)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true);
                message.AppendInteger(rooms.Count);

                foreach (RoomData room in rooms)
                {
                    message.AppendInteger(room.Id);
                    message.AppendString(room.Name);
                    message.AppendBool(false);
                }
                session.Send(message);
            }
        }
    }
}