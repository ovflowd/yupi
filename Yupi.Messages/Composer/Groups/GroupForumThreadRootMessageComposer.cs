using System;
using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupForumThreadRootMessageComposer : Yupi.Messages.Contracts.GroupForumThreadRootMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int groupId, int startIndex, IList<GroupForumPost> threads)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groupId);
				message.AppendInteger(startIndex);
				message.AppendInteger(threads.Count);

				foreach (GroupForumPost thread in threads)
				{
					message.AppendInteger(thread.Id);
					message.AppendInteger(thread.PosterId);
					message.AppendString(thread.PosterName);
					message.AppendString(thread.Subject);
					message.AppendBool(thread.Pinned);
					message.AppendBool(thread.Locked);
					message.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
					message.AppendInteger(thread.MessageCount + 1);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendString(string.Empty);
					message.AppendInteger(Yupi.GetUnixTimeStamp() - thread.Timestamp);
					message.AppendByte(thread.Hidden ? 10 : 1);
					message.AppendInteger(0);
					message.AppendString(thread.Hider);
					message.AppendInteger(0);
				}
				session.Send (message);
			}
		}
	}
}

