using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Groups
{
	public class GroupForumNewResponseMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int messageCount, Habbo user, int timestamp, string content) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(threadId);
				message.AppendInteger(messageCount);
				message.AppendInteger(0);
				message.AppendInteger(user.Id);
				message.AppendString(user.UserName);
				message.AppendString(user.Look);
				message.AppendInteger(Yupi.GetUnixTimeStamp() - timestamp);
				message.AppendString(content);
				message.AppendByte(0);
				message.AppendInteger(0);
				message.AppendString(string.Empty);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

