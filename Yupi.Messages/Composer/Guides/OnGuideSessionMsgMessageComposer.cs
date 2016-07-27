using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	// TODO Rename
	public class OnGuideSessionMsgMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISender session, GameClient requester, string content, int userId) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(message);
				message.AppendInteger(session.GetHabbo().Id);
				session.Send (message);
				requester.Send (message);
			}
		}
	}
}

