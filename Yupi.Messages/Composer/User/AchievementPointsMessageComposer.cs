using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class AchievementPointsMessageComposer : Yupi.Messages.Contracts.AchievementPointsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int points)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (points);
				session.Send (message);
			}
		}
	}
}

