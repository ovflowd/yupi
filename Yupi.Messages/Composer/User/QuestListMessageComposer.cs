using System;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.User
{
	public class QuestListMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (0); // TODO What do these values mean?
				message.AppendBool (true);
				session.Send (message);
			}
		}
	}
}

