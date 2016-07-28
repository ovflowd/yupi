using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterGamesLeftMessageComposer : AbstractComposerVoid
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			// TODO  hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(18);
				message.AppendInteger(-1);
				message.AppendInteger(0);
			}
		}
	}
}

