namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class AchievementPointsMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int points)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}