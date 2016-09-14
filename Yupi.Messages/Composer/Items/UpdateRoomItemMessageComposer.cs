using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class UpdateRoomItemMessageComposer : Contracts.UpdateRoomItemMessageComposer
    {
        public override void Compose(ISender session, FloorItem item)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
                //item.Serialize(message);
                session.Send(message);
            }
        }
    }
}