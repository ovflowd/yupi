using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class RemoveInventoryObjectMessageComposer : Contracts.RemoveInventoryObjectMessageComposer
    {
        public override void Compose(ISender session, int itemId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }
    }
}