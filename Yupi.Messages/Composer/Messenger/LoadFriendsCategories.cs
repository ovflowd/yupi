using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
	public class LoadFriendsCategories : AbstractComposerVoid
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(2000);
				message.AppendInteger(300);
				message.AppendInteger(800);
				message.AppendInteger(1100);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

