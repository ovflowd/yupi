using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class GroupForumThreadUpdateMessageComposer : Yupi.Messages.Contracts.GroupForumThreadUpdateMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int groupId, GroupForumThread thread, bool pin, bool Lock)
		{
			// TODO Hardcoded message
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(thread.Id);
				message.AppendInteger(thread.Creator.Id);
				message.AppendString(thread.Creator.UserName);
				message.AppendString(thread.Subject);
				message.AppendBool(thread.Pinned);
				message.AppendBool(thread.Locked);
				message.AppendInteger((int)(DateTime.Now - thread.CreatedAt).TotalSeconds);
				message.AppendInteger(thread.Posts.Count);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(1);
				message.AppendString(string.Empty);
				message.AppendInteger((int)(DateTime.Now - thread.CreatedAt).TotalSeconds);
				message.AppendByte(thread.Hidden ? 10 : 1);
				message.AppendInteger(1);
				message.AppendString(thread.HiddenBy.UserName);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

