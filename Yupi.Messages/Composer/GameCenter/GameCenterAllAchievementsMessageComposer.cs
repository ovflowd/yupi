using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterAllAchievementsMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session)
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

