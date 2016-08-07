using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SpectatorModeMessageComposer : Yupi.Messages.Contracts.SpectatorModeMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				session.Send (message);
			}
		}
	}
}

