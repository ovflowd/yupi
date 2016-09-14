using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class WardrobeMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<LoadWardrobeMessageComposer>().Compose(session, session.Info.Inventory.Wardrobe);
        }
    }
}