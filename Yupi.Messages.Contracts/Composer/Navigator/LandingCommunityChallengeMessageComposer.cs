using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LandingCommunityChallengeMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int onlineFriends)
        {
            // Do nothing by default.
        }
    }
}