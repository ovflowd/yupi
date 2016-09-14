using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class ReadForumThreadMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public ReadForumThreadMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var threadId = request.GetInteger();
            var startIndex = request.GetInteger();

            request.GetInteger(); // TODO Unused

            var theGroup = GroupRepository.FindBy(groupId);

            if (theGroup == null) return;

            var thread = theGroup.Forum.GetThread(threadId);

            if (thread == null) return;

            // TODO Magic constant
            var posts = thread.Posts.Skip(startIndex).Take(20).ToList();

            router.GetComposer<GroupForumReadThreadMessageComposer>()
                .Compose(session, groupId, threadId, startIndex, posts);
        }
    }
}