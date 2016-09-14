namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public abstract class UnlockAchievementMessageComposer : AbstractComposer<Achievement, int, int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Achievement achievement, int level, int pointReward,
            int pixelReward)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}