using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class LandingCommunityChallengeMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int onlineFriends)
        {
            // Do nothing by default.
        }
    }
}