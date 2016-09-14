using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class CompleteSafetyQuizMessageEvent : AbstractHandler
    {
        private readonly AchievementManager AchievementManager;

        public CompleteSafetyQuizMessageEvent()
        {
            AchievementManager = DependencyFactory.Resolve<AchievementManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            AchievementManager.ProgressUserAchievement(session, "ACH_SafetyQuizGraduate", 1);
        }
    }
}