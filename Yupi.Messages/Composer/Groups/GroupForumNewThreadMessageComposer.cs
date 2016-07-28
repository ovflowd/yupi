using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GroupForumNewThreadMessageComposer : AbstractComposer
	{
		// TODO Hardcoded
		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int groupId, int threadId, int habboId, string subject, string content, int timestamp) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(threadId);
				message.AppendInteger(habboId);
				message.AppendString(subject);
				message.AppendString(content);
				message.AppendBool(false);
				message.AppendBool(false);
				message.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
				message.AppendInteger(1);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(1);
				message.AppendString(string.Empty);
				message.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
				message.AppendByte(1);
				message.AppendInteger(1);
				message.AppendString(string.Empty);
				message.AppendInteger(42);
				session.Send (message);
			}
		}
	}
}

