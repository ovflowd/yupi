using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class RequestLeaveGroupMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public RequestLeaveGroupMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var userId = request.GetInteger();

            var group = GroupRepository.FindBy(groupId);

            if ((group == null) || (group.Creator.Id == userId))
                return;

            if ((userId == session.Info.Id) || group.Admins.Contains(session.Info))
                router.GetComposer<GroupAreYouSureMessageComposer>().Compose(session, userId);
        }
    }
}