using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class DoorbellMessageComposer : AbstractComposer<string>
	{
		public override void Compose (Yupi.Protocol.ISender session, string username)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString (username); 
				session.Send (message);
			}
		}
	}
}

