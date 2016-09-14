using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class HotelViewCountdownMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var time = message.GetString();
            router.GetComposer<HotelViewCountdownMessageComposer>().Compose(session, time);
        }
    }
}