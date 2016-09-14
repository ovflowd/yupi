using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
    public class GroupForumReadThreadMessageComposer : Yupi.Messages.Contracts.GroupForumReadThreadMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, int groupId, int threadId, int startIndex,
            List<GroupForumPost> posts)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                message.AppendInteger(threadId);
                message.AppendInteger(startIndex);
                message.AppendInteger(posts.Count);

                foreach (GroupForumPost post in posts)
                {
                    message.AppendInteger(post.Id);
                    message.AppendInteger(post.Id);
                    message.AppendInteger(post.Poster.Id);
                    message.AppendString(post.Poster.Name);
                    message.AppendString(post.Poster.Look);
                    message.AppendInteger((int) (DateTime.Now - post.Timestamp).TotalSeconds);
                    message.AppendString(post.Content);
                    message.AppendByte(0); // TODO What are these values?
                    message.AppendInteger(post.HiddenBy.Id);
                    message.AppendString(post.HiddenBy.Name);
                    message.AppendInteger(0);
                    message.AppendInteger(0);
                }
                session.Send(message);
            }
        }
    }
}