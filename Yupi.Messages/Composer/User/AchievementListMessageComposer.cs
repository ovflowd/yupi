using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
    public class AchievementListMessageComposer : Yupi.Messages.Contracts.AchievementListMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<UserAchievement> achievements)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(achievements.Count);

                foreach (UserAchievement achievement in achievements)
                {
                    AchievementLevel nextLevel = achievement.Achievement.NextLevel(achievement.Level);

                    message.AppendInteger(achievement.Id);
                    message.AppendInteger(nextLevel.Level);
                    message.AppendString(achievement.Achievement.GroupName + achievement.Level.Level);
                    message.AppendInteger(achievement.Level.Requirement);
                    message.AppendInteger(nextLevel.Requirement);
                    message.AppendInteger(nextLevel.RewardPoints);
                    message.AppendInteger(0);
                    message.AppendInteger(achievement.Progress);
                    message.AppendBool(achievement.Level.Level == achievement.Achievement.GetMaxLevel());
                    message.AppendString(achievement.Achievement.Category);
                    message.AppendString(string.Empty);
                    message.AppendInteger(achievement.Achievement.GetMaxLevel());
                    message.AppendInteger(0);
                }

                message.AppendString(string.Empty);
                session.Send(message);
            }
        }
    }
}