using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class EnableNotificationsMessageComposer : Yupi.Messages.Contracts.EnableNotificationsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true); //isOpen
				message.AppendBool(false);
				session.Send (message);
			}
		}	
	}
}

