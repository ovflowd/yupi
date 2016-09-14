using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{ // TODO Refactor

    public class NewInventoryObjectMessageComposer : Contracts.NewInventoryObjectMessageComposer
    {
        // TODO Remove...
        public override void Compose(ISender session, int itemId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(1);
                message.AppendInteger(1);
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }

        public override void Compose(ISender session, BaseItem item, List<Item> list)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1); // TODO Hardcoded value

                message.AppendInteger((int) item.Type);
                message.AppendInteger(list.Count);

                foreach (var current in list)
                    message.AppendInteger(current.Id);
                session.Send(message);
            }
        }
    }
}