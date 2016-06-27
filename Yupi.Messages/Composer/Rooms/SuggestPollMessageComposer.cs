using System;
using Yupi.Emulator.Game.Polls;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SuggestPollMessageComposer : AbstractComposer<Poll>
	{
		public override void Compose (Yupi.Protocol.ISender session, Poll poll)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				poll.Serialize(message);
				session.Send (message);
			}
		}
	}
}

