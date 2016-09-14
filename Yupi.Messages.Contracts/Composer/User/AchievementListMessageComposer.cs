using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class AchievementListMessageComposer : AbstractComposer<IList<UserAchievement>>
    {
        public override void Compose(ISender session, IList<UserAchievement> achievements)
        {
            // Do nothing by default.
        }
    }
}