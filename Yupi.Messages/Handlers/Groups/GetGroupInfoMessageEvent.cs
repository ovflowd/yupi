using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GetGroupInfoMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GetGroupInfoMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var newWindow = request.GetBool();

            var group = GroupRepository.FindBy(groupId);

            if (group == null)
                return;

            router.GetComposer<GroupDataMessageComposer>().Compose(session, group, session.Info, newWindow);
        }
    }
}