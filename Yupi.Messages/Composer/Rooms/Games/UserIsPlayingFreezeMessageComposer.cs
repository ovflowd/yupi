using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Games
{
	public class UserIsPlayingFreezeMessageComposer : AbstractComposer<bool>
	{
		public override void Compose ( Yupi.Protocol.ISender session, bool isPlaying)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(isPlaying);
				session.Send (message);
			}
		}
	}
}

