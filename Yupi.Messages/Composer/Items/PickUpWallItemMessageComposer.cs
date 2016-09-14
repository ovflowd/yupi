using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class PickUpWallItemMessageComposer : Contracts.PickUpWallItemMessageComposer
    {
        public override void Compose(ISender session, WallItem item, int pickerId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(item.Id.ToString());
                message.AppendInteger(pickerId);
                session.Send(message);
            }
        }
    }
}