using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
    // TODO Rename
    public class CancellInviteGuide : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;

        public CancellInviteGuide()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            router.GetComposer<OnGuideSessionDetachedMessageComposer>().Compose(session, 2);

            AchievementManager.ProgressUserAchievement(session, "ACH_GuideFeedbackGiver", 1);
        }
    }
}