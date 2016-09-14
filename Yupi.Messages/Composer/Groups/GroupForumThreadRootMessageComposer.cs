namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class GroupForumThreadRootMessageComposer : Yupi.Messages.Contracts.GroupForumThreadRootMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int groupId, int startIndex,
            IList<GroupForumThread> threads)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(groupId);
                message.AppendInteger(startIndex);
                message.AppendInteger(threads.Count);

                foreach (GroupForumThread thread in threads)
                {
                    message.AppendInteger(thread.Id);
                    message.AppendInteger(thread.Creator.Id);
                    message.AppendString(thread.Creator.Name);
                    message.AppendString(thread.Subject);
                    message.AppendBool(thread.Pinned);
                    message.AppendBool(thread.Locked);
                    message.AppendInteger((int) (DateTime.Now - thread.CreatedAt).TotalSeconds);
                    message.AppendInteger(thread.Posts.Count);
                    message.AppendInteger(0);
                    message.AppendInteger(0); // TODO Unknown
                    message.AppendInteger(0);
                    message.AppendString(string.Empty);
                    message.AppendInteger((int) (DateTime.Now - thread.CreatedAt).TotalSeconds);
                    message.AppendByte(thread.Hidden ? 10 : 1);
                    message.AppendInteger(thread.HiddenBy.Id);
                    message.AppendString(thread.HiddenBy.Name);
                    message.AppendInteger(0);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}