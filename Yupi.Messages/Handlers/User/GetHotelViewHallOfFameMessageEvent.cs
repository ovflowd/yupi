using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class GetHotelViewHallOfFameMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var code = message.GetString();

            router.GetComposer<HotelViewHallOfFameMessageComposer>().Compose(session, code);
        }
    }
}