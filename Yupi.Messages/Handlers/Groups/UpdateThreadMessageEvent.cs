using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Messages.Contracts;


namespace Yupi.Messages.Groups
{
    public class UpdateThreadMessageEvent : AbstractHandler
    {
        private IRepository<Group> GroupRepository;

        public UpdateThreadMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int threadId = request.GetInteger();
            bool pin = request.GetBool();
            bool Lock = request.GetBool();

            Group theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null)
            {
                return;
            }

            GroupForumThread thread = theGroup.Forum.GetThread(threadId);

            if (thread.Creator == session.Info || theGroup.Admins.Contains(session.Info))
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