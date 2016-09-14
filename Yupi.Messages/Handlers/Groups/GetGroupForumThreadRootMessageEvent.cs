namespace Yupi.Messages.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetGroupForumThreadRootMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GetGroupForumThreadRootMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();

            int startIndex = request.GetInteger();

            Group theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null)
            {
                return;
            }

            // TODO Magic constant!
            List<GroupForumThread> threads = theGroup.Forum.Threads.Skip(startIndex).Take(20).ToList();

            router.GetComposer<GroupForumThreadRootMessageComposer>().Compose(session, groupId, startIndex, threads);
        }

        #endregion Methods
    }
}