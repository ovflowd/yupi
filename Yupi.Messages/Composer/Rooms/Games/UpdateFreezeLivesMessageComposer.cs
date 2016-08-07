using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Games
{
	public class UpdateFreezeLivesMessageComposer : Yupi.Messages.Contracts.UpdateFreezeLivesMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int roomId, int lives)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				message.AppendInteger(lives);
				session.Send (message);
			}
		}
	}
}

