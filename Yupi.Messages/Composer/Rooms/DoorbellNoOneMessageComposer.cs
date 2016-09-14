using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class DoorbellNoOneMessageComposer : Yupi.Messages.Contracts.DoorbellNoOneMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (string.Empty); // TODO What can this be used for?
				session.Send (message);
			}
		}
	}
}

