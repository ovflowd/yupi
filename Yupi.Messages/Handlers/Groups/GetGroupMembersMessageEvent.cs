using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GetGroupMembersMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GetGroupMembersMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var page = request.GetInteger();
            var searchVal = request.GetString();
            var reqType = request.GetUInt32();

            var group = GroupRepository.FindBy(groupId);

            if (group == null) return;

            router.GetComposer<GroupMembersMessageComposer>()
                .Compose(session, group, reqType, session.Info, searchVal, page);
        }
    }
}