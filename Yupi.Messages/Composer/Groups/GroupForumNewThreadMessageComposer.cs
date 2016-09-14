using System;
using Yupi.Protocol;

namespace Yupi.Messages.Groups
{
    public class GroupForumNewThreadMessageComposer : Contracts.GroupForumNewThreadMessageComposer
    {
        // TODO Hardcoded
        public override void Compose(ISender session, int groupId, int threadId, int habboId, string subject,
            string content, int timestamp)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                throw new NotImplementedException();
                /*
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
                */
            }
        }
    }
}