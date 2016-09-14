using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class UpdateInventoryMessageComposer : Contracts.UpdateInventoryMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }
    }
}