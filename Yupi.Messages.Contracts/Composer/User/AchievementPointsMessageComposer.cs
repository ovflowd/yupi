using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class AchievementPointsMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int points)
        {
            // Do nothing by default.
        }
    }
}