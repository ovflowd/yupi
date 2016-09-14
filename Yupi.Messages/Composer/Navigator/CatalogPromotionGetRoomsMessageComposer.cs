namespace Yupi.Messages.Navigator
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class CatalogPromotionGetRoomsMessageComposer : Yupi.Messages.Contracts.CatalogPromotionGetRoomsMessageComposer
    {
        #region Methods

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

        #endregion Methods
    }
}