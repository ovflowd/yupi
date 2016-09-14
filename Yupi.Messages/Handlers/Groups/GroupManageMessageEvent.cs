using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GroupManageMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GroupManageMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var group = GroupRepository.FindBy(groupId);

            if (group == null)
                return;

            // TODO Hardcoded value! (should use user rights instead of rank!)
            if (group.Admins.Contains(session.Info) || (group.Creator != session.Info) ||
                session.Info.HasPermission("fuse_manage_any_group"))
                router.GetComposer<GroupDataEditMessageComposer>().Compose(session, group);
        }
    }
}