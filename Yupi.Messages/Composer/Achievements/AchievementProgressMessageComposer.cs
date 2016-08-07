using System;

using Yupi.Protocol;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Achievements
{
	public class AchievementProgressMessageComposer : Contracts.AchievementProgressMessageComposer
	{
		public override void Compose( Yupi.Protocol.ISender session, UserAchievement userAchievement) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(userAchievement.Achievement.Id);
				message.AppendInteger(userAchievement.Level.Level);
				message.AppendString(userAchievement.Achievement.GroupName + userAchievement.Level.Level);
				message.AppendInteger(userAchievement.Level.Requirement);
				message.AppendInteger(userAchievement.Level.Requirement);
				message.AppendInteger(userAchievement.Level.RewardPixels);
				message.AppendInteger(0);
				message.AppendInteger(userAchievement.Progress);
				message.AppendBool(userAchievement.Level.Level >= userAchievement.Achievement.GetMaxLevel());
				message.AppendString(userAchievement.Achievement.Category);
				message.AppendString(string.Empty);
				message.AppendInteger(userAchievement.Achievement.GetMaxLevel());
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

