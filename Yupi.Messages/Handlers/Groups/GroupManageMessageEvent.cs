namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GroupManageMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GroupManageMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            Group group = GroupRepository.FindBy(groupId);

            if (group == null)
                return;

            // TODO Hardcoded value! (should use user rights instead of rank!)
            if (group.Admins.Contains(session.Info) || group.Creator != session.Info ||
                session.Info.HasPermission("fuse_manage_any_group"))
            {
                router.GetComposer<GroupDataEditMessageComposer>().Compose(session, group);
            }
        }

        #endregion Methods
    }
}