namespace Yupi.Messages.Contracts
{
    using System;

    using Yupi.Model.Domain;

    public abstract class AchievementProgressMessageComposer : AbstractComposer<UserAchievement>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserAchievement userAchievement)
        {
        }

        #endregion Methods
    }
}