using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class LoadPetInventoryMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            session.Router.GetComposer<PetInventoryMessageComposer>().Compose(session, session.Info.Inventory.Pets);
        }
    }
}