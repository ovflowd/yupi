using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class GroupUpdateNameMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;

        public GroupUpdateNameMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        // TODO Refactor
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var name = request.GetString();
            var description = request.GetString();

            var theGroup = GroupRepository.FindBy(groupId);

            if (theGroup?.Creator != session.Info)
                return;

            theGroup.Name = name;
            theGroup.Description = description;

            GroupRepository.Save(theGroup);

            if (session.Room != null)
                session.Room.EachUser(
                    entitySession =>
                    {
                        entitySession.Router.GetComposer<GroupDataMessageComposer>()
                            .Compose(entitySession, theGroup, session.Info);
                    });
            else router.GetComposer<GroupDataMessageComposer>().Compose(session, theGroup, session.Info);
        }
    }
}