namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class LandingCommunityChallengeMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int onlineFriends)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}