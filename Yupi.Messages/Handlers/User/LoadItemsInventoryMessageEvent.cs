using Yupi.Messages.Items;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class LoadItemsInventoryMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<LoadInventoryMessageComposer>().Compose(session, session.Info.Inventory);
        }
    }
}