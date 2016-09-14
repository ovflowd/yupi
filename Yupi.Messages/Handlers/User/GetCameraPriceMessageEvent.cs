using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetCameraPriceMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            // TODO Replace hardcoded values
            router.GetComposer<SetCameraPriceMessageComposer>().Compose(session, 0, 10);
        }
    }
}