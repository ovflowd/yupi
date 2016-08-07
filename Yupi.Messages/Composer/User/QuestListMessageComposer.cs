using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class QuestListMessageComposer : Yupi.Messages.Contracts.QuestListMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0); // TODO What do these values mean?
				message.AppendBool (true);
				session.Send (message);
			}
		}
	}
}

