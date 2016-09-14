using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GetGroupForumDataMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GetGroupForumDataMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();

            var theGroup = GroupRepository.FindBy(groupId);

            if (theGroup != null)
                router.GetComposer<GroupForumDataMessageComposer>().Compose(session, theGroup, session.Info);
        }
    }
}