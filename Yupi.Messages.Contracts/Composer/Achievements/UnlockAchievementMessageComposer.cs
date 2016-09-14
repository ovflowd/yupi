using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UnlockAchievementMessageComposer : AbstractComposer<Achievement, int, int, int>
    {
        public override void Compose(ISender session, Achievement achievement, int level, int pointReward,
            int pixelReward)
        {
            // Do nothing by default.
        }
    }
}