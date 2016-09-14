namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetGroupMembersMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GetGroupMembersMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int page = request.GetInteger();
            string searchVal = request.GetString();
            uint reqType = request.GetUInt32();

            Group group = GroupRepository.FindBy(groupId);

            if (group == null)
            {
                return;
            }

            router.GetComposer<GroupMembersMessageComposer>()
                .Compose(session, group, reqType, session.Info, searchVal, page);
        }

        #endregion Methods
    }
}