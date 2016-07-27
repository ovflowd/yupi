using System;
using Yupi.Protocol;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Achievements
{
	public class UnlockAchievementMessageComposer : AbstractComposer
	{
		public void Compose(ISender session, Achievement achievement, uint level, uint pointReward, uint pixelReward) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(achievement.Id);
				message.AppendInteger(level);
				message.AppendInteger(144);
				message.AppendString($"{achievement.GroupName}{level}");
				message.AppendInteger(pointReward);
				message.AppendInteger(pixelReward);
				message.AppendInteger(0);
				message.AppendInteger(10);
				message.AppendInteger(21);
				message.AppendString(level > 1 ? $"{achievement.GroupName}{level - 1}" : string.Empty);
				message.AppendString(achievement.Category);
				message.AppendBool(true);

				session.Send (message);
			}
		}
	}
}

