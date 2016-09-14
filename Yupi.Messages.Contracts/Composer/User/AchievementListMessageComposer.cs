namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class AchievementListMessageComposer : AbstractComposer<IList<UserAchievement>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<UserAchievement> achievements)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}