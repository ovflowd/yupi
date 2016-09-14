using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Navigator
{
    public class CatalogPromotionGetRoomsMessageComposer : Contracts.CatalogPromotionGetRoomsMessageComposer
    {
        public override void Compose(ISender session, IList<RoomData> rooms)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(true);
                message.AppendInteger(rooms.Count);

                foreach (var room in rooms)
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