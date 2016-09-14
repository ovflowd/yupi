namespace Yupi.Messages.Groups
{
    using System;
    using System.Data;

    using Yupi.Messages.Notification;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class AlterForumThreadStateMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public AlterForumThreadStateMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int threadId = request.GetInteger();
            int stateToSet = request.GetInteger();

            Group theGroup = GroupRepository.FindBy(groupId);

            if (theGroup != null)
            {
                GroupForumThread thread = theGroup.Forum.GetThread(threadId);

                if (thread != null && (thread.Creator == session.Info || theGroup.Admins.Contains(session.Info)))
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

        #endregion Methods
    }
}