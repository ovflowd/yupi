using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
	public class AchievementListMessageComposer : AbstractComposer<Habbo, List<Achievement>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Habbo user, List<Achievement> achievements)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(achievements.Count);

				foreach (Achievement achievement in achievements)
				{
					UserAchievement achievementData = user.GetAchievementData(achievement.GroupName);

					uint i = achievementData?.Level + 1 ?? 1;

					uint count = (uint) achievement.Levels.Count;

					if (i > count)
						i = count;

					AchievementLevel achievementLevel = achievement.Levels[i];

					AchievementLevel oldLevel = achievement.Levels.ContainsKey(i - 1) ? achievement.Levels[i - 1] : achievementLevel;

					message.AppendInteger(achievement.Id);
					message.AppendInteger(i);
					message.AppendString($"{achievement.GroupName}{i}");
					message.AppendInteger(oldLevel.Requirement);
					message.AppendInteger(achievementLevel.Requirement);
					message.AppendInteger(achievementLevel.RewardPoints);
					message.AppendInteger(0);
					message.AppendInteger(achievementData?.Progress ?? 0);
					message.AppendBool(!(achievementData == null || achievementData.Level < count));
					message.AppendString(achievement.Category);
					message.AppendString(string.Empty);
					message.AppendInteger(count);
					message.AppendInteger(0);
				}

				message.AppendString(string.Empty);
				session.Send(message);
			}
		}
	}
}

