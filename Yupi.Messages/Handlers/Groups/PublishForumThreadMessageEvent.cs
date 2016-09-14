using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class PublishForumThreadMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public PublishForumThreadMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var threadId = request.GetInteger();
            var subject = request.GetString();
            var content = request.GetString();

            var group = GroupRepository.FindBy(groupId);

            if (group == null)
                return;

            GroupForumThread thread;

            if (threadId == 0)
            {
                // New thread
                thread = new GroupForumThread();
            }
            else
            {
                thread = group.Forum.GetThread(threadId);

                if (thread == null) return;
            }

            if (thread.Locked || thread.Hidden)
                return;

            var post = new GroupForumPost
            {
                Content = content,
                Subject = subject,
                Poster = session.Info
            };

            group.Forum.ForumScore += 0.25;
            // TODO SAVE
            throw new NotImplementedException();
            /*
            group.UpdateForum();

            if (threadId == 0)
            {
                router.GetComposer<GroupForumNewThreadMessageComposer> ().Compose (session, groupId, threadId, session.GetHabbo ().Id, subject, content, timestamp);
            }
            else
            {
                router.GetComposer<GroupForumNewResponseMessageComposer> ().Compose (
                    session, groupId, threadId, group.Forum.ForumMessagesCount, session.GetHabbo (), timestamp);
            }*/
        }
    }
}