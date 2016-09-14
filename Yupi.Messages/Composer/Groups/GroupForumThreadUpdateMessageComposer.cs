using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupForumThreadUpdateMessageComposer : Contracts.GroupForumThreadUpdateMessageComposer
    {
        public override void Compose(ISender session, int groupId, GroupForumThread thread, bool pin, bool Lock)
        {
            // TODO Hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                message.AppendInteger(thread.Id);
                message.AppendInteger(thread.Creator.Id);
                message.AppendString(thread.Creator.Name);
                message.AppendString(thread.Subject);
                message.AppendBool(thread.Pinned);
                message.AppendBool(thread.Locked);
                message.AppendInteger((int) (DateTime.Now - thread.CreatedAt).TotalSeconds);
                message.AppendInteger(thread.Posts.Count);
                message.AppendInteger(0);
                message.AppendInteger(0);
                message.AppendInteger(1);
                message.AppendString(string.Empty);
                message.AppendInteger((int) (DateTime.Now - thread.CreatedAt).TotalSeconds);
                message.AppendByte(thread.Hidden ? 10 : 1);
                message.AppendInteger(1);
                message.AppendString(thread.HiddenBy.Name);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}