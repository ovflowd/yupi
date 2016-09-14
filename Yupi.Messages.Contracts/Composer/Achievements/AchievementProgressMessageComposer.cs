using System;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class AchievementProgressMessageComposer : AbstractComposer<UserAchievement>
    {
        public override void Compose(Yupi.Protocol.ISender session, UserAchievement userAchievement)
        {
        }
    }
}