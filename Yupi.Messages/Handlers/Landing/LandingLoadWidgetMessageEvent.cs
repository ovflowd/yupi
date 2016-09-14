using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Landing
{
    public class LandingLoadWidgetMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var text = request.GetString();

            router.GetComposer<LandingWidgetMessageComposer>().Compose(session, text);
        }
    }
}