using Yupi.Messages.Notification;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class AlterForumThreadStateMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public AlterForumThreadStateMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var threadId = request.GetInteger();
            var stateToSet = request.GetInteger();

            var theGroup = GroupRepository.FindBy(groupId);

            if (theGroup != null)
            {
                var thread = theGroup.Forum.GetThread(threadId);

                if ((thread != null) && ((thread.Creator == session.Info) || theGroup.Admins.Contains(session.Info)))
                {
                    thread.Hidden = stateToSet == 20;
                    thread.HiddenBy = session.Info;

                    GroupRepository.Save(theGroup);

                    router.GetComposer<SuperNotificationMessageComposer>()
                        .Compose(session, string.Empty, string.Empty, string.Empty, string.Empty,
                            stateToSet == 20 ? "forums.thread.hidden" : "forums.thread.restored", 0);

                    router.GetComposer<GroupForumThreadUpdateMessageComposer>()
                        .Compose(session, groupId, thread, thread.Pinned, thread.Locked);
                }
            }
        }
    }
}