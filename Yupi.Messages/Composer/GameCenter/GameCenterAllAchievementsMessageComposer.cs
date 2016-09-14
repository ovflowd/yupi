using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterAllAchievementsMessageComposer : Yupi.Messages.Contracts.GameCenterAllAchievementsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			// TODO  hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(0); //count

				message.AppendInteger(0); //gameId
				message.AppendInteger(0); //count
				message.AppendInteger(0); //achId
				message.AppendString("SnowWarTotalScore"); //achName
				message.AppendInteger(0); //levels

				session.Send (message);
			}
		}
	}
}

