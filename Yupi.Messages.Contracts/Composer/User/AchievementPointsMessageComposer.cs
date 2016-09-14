using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class AchievementPointsMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int points)
        {
            // Do nothing by default.
        }
    }
}