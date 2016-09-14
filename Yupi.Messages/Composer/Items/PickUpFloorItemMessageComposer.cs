using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class PickUpFloorItemMessageComposer : Contracts.PickUpFloorItemMessageComposer
    {
        public override void Compose(ISender session, FloorItem item, int pickerId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendBool(false); //expired
                message.AppendInteger(pickerId);
                message.AppendInteger(0); // delay
                session.Send(message);
            }
        }
    }
}