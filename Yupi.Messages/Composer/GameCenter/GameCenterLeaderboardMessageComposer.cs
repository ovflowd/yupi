using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.GameCenter
{
	public class GameCenterLeaderboardMessageComposer : AbstractComposerVoid
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			// TODO hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (2014);
				message.AppendInteger (49);
				message.AppendInteger (0);
				message.AppendInteger (0);
				message.AppendInteger (6526);
				message.AppendInteger (1);
				message.AppendInteger (session.GetHabbo ().Id);
				message.AppendInteger (0);
				message.AppendInteger (1);
				message.AppendString (session.GetHabbo ().UserName);
				message.AppendString (session.GetHabbo ().Look);
				message.AppendString (session.GetHabbo ().Gender);
				message.AppendInteger (1);
				message.AppendInteger (18);

				session.Send (message);
			}
		}
	}
}

