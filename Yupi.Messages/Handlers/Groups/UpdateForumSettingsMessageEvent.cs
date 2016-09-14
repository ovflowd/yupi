using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class UpdateForumSettingsMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public UpdateForumSettingsMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var whoCanRead = request.GetUInt32();
            var whoCanPost = request.GetUInt32();
            var whoCanThread = request.GetUInt32();
            var whoCanMod = request.GetUInt32();

            var group = GroupRepository.FindBy(groupId);

            if (group?.Creator != session.Info)
                return;

            // TODO Check rights?!
            group.Forum.WhoCanRead = whoCanRead;
            group.Forum.WhoCanPost = whoCanPost;
            group.Forum.WhoCanThread = whoCanThread;
            group.Forum.WhoCanMod = whoCanMod;

            GroupRepository.Save(group);
            router.GetComposer<GroupForumDataMessageComposer>().Compose(session, group, session.Info);
        }
    }
}