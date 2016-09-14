using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class AddFloorItemMessageComposer : Contracts.AddFloorItemMessageComposer
    {
        public override void Compose(ISender room, FloorItem item)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                /*
                item.Serialize(message);
                message.AppendString(room.Data.Owner.Name);
                */
                throw new NotImplementedException();
                room.Send(message);
            }
        }
    }
}