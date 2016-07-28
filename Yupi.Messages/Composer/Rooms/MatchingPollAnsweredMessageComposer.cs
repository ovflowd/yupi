using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class MatchingPollAnsweredMessageComposer : AbstractComposer<uint, string>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, uint userId, string text)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(userId);
				message.AppendString(text);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

