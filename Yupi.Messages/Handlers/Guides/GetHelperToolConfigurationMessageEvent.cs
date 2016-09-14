using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    public class GetHelperToolConfigurationMessageEvent : AbstractHandler
    {
        private readonly GuideManager GuideManager;

        public GetHelperToolConfigurationMessageEvent()
        {
            GuideManager = DependencyFactory.Resolve<GuideManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var onDuty = message.GetBool();

            // TODO Use these values
            message.GetBool();
            message.GetBool();
            message.GetBool();

            if (onDuty)
                GuideManager.Add(session);
            else
                GuideManager.Remove(session);

            router.GetComposer<HelperToolConfigurationMessageComposer>().Compose(session, onDuty,
                GuideManager.Guides.Count, GuideManager.Helpers.Count, GuideManager.Guardians.Count);
        }
    }
}