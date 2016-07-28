using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	// TODO Rename
	public class OnGuideSessionMsgMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> requester, string content, int userId) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(content);
				message.AppendInteger(userId);
				session.Send (message);
				requester.Send (message);
			}
		}
	}
}

