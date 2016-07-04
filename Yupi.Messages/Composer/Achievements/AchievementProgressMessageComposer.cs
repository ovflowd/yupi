using System;
using Yupi.Emulator.Game.Achievements.Structs;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Achievements
{
	public class AchievementProgressMessageComposer : AbstractComposer
	{
		public void Compose(ISender session, Achievement achievement, uint targetLevel,
			AchievementLevel targetLevelData, uint totalLevels, UserAchievement userData) {

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(achievement.Id);
				message.AppendInteger(targetLevel);
				message.AppendString($"{achievement.GroupName}{targetLevel}");
				message.AppendInteger(targetLevelData.Requirement);
				message.AppendInteger(targetLevelData.Requirement);
				message.AppendInteger(targetLevelData.RewardPixels);
				message.AppendInteger(0);
				message.AppendInteger(userData.Progress);
				message.AppendBool(userData.Level >= totalLevels);
				message.AppendString(achievement.Category);
				message.AppendString(string.Empty);
				message.AppendInteger(totalLevels);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

