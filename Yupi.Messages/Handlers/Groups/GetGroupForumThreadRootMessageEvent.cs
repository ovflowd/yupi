using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GetGroupForumThreadRootMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GetGroupForumThreadRootMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();

            var startIndex = request.GetInteger();

            var theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null) return;

            // TODO Magic constant!
            var threads = theGroup.Forum.Threads.Skip(startIndex).Take(20).ToList();

            router.GetComposer<GroupForumThreadRootMessageComposer>().Compose(session, groupId, startIndex, threads);
        }
    }
}