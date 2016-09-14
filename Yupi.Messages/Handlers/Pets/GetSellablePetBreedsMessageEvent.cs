using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
    public class GetSellablePetBreedsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var type = request.GetString();
            router.GetComposer<SellablePetBreedsMessageComposer>().Compose(session, type);
        }
    }
}