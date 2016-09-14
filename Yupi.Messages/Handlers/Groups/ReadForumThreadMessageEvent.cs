namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class ReadForumThreadMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public ReadForumThreadMessageEvent()
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
            int startIndex = request.GetInteger();

            request.GetInteger(); // TODO Unused

            Group theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null)
            {
                return;
            }

            GroupForumThread thread = theGroup.Forum.GetThread(threadId);

            if (thread == null)
            {
                return;
            }

            // TODO Magic constant
            List<GroupForumPost> posts = thread.Posts.Skip(startIndex).Take(20).ToList();

            router.GetComposer<GroupForumReadThreadMessageComposer>()
                .Compose(session, groupId, threadId, startIndex, posts);
        }

        #endregion Methods
    }
}