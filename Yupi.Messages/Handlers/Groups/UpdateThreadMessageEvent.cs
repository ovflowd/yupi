using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class UpdateThreadMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public UpdateThreadMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var threadId = request.GetInteger();
            var pin = request.GetBool();
            var Lock = request.GetBool();

            var theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null) return;

            var thread = theGroup.Forum.GetThread(threadId);

            if ((thread.Creator == session.Info) || theGroup.Admins.Contains(session.Info))
            {
                thread.Locked = Lock;
                thread.Pinned = pin;
            }

            //GroupForumPost thread = new GroupForumPost(row);
            throw new NotImplementedException();
            /*
            if (thread.Pinned != pin) {
                router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty, string.Empty, string.Empty, string.Empty,
                    pin ? "forums.thread.pinned" : "forums.thread.unpinned", 0);
            }

            if (thread.Locked != Lock) {
                router.GetComposer<SuperNotificationMessageComposer> ().Compose (session, string.Empty, string.Empty, string.Empty, string.Empty,
                    Lock ? "forums.thread.locked" : "forums.thread.unlocked", 0);
            }

            router.GetComposer<GroupForumThreadUpdateMessageComposer> ().Compose (session, groupId, thread, pin, Lock);
*/
        }
    }
}