using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;


namespace Yupi.Messages.Items
{ // TODO Refactor

    public class NewInventoryObjectMessageComposer : Yupi.Messages.Contracts.NewInventoryObjectMessageComposer
    {
        // TODO Remove...
        public override void Compose(Yupi.Protocol.ISender session, int itemId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(1);
                message.AppendInteger(1);
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }

        public override void Compose(Yupi.Protocol.ISender session, BaseItem item, List<Item> list)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1); // TODO Hardcoded value

                message.AppendInteger((int) item.Type);
                message.AppendInteger(list.Count);

                foreach (Item current in list)
                    message.AppendInteger(current.Id);
                session.Send(message);
            }
        }
    }
}