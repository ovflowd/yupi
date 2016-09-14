using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class AchievementProgressMessageComposer : AbstractComposer<UserAchievement>
    {
        public override void Compose(ISender session, UserAchievement userAchievement)
        {
        }
    }
}