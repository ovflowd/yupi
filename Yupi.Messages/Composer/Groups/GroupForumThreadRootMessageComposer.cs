using System;
using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupForumThreadRootMessageComposer : Yupi.Messages.Contracts.GroupForumThreadRootMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int groupId, int startIndex, IList<GroupForumThread> threads)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(startIndex);
				message.AppendInteger(threads.Count);

				foreach (GroupForumThread thread in threads)
				{
					message.AppendInteger(thread.Id);
					message.AppendInteger(thread.Creator.Id);
					message.AppendString(thread.Creator.UserName);
					message.AppendString(thread.Subject);
					message.AppendBool(thread.Pinned);
					message.AppendBool(thread.Locked);
					message.AppendInteger((int)(DateTime.Now - thread.CreatedAt).TotalSeconds);
					message.AppendInteger(thread.Posts.Count);
					message.AppendInteger(0);
					message.AppendInteger(0); // TODO Unknown
					message.AppendInteger(0);
					message.AppendString(string.Empty);
					message.AppendInteger((int)(DateTime.Now - thread.CreatedAt).TotalSeconds);
					message.AppendByte(thread.Hidden ? 10 : 1);
					message.AppendInteger(thread.HiddenBy.Id);
					message.AppendString(thread.HiddenBy.UserName);
					message.AppendInteger(0);
				}
				session.Send (message);
			}
		}
	}
}

