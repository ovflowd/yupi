namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GroupUpdateNameMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GroupUpdateNameMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        // TODO Refactor
        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            string name = request.GetString();
            string description = request.GetString();

            Group theGroup = GroupRepository.FindBy(groupId);

            if (theGroup?.Creator != session.Info)
                return;

            theGroup.Name = name;
            theGroup.Description = description;

            GroupRepository.Save(theGroup);

            if (session.Room != null)
            {
                session.Room.EachUser(
                    (entitySession) =>
                    {
                        entitySession.Router.GetComposer<GroupDataMessageComposer>()
                            .Compose(entitySession, theGroup, session.Info);
                    });
            }
            else
            {
                router.GetComposer<GroupDataMessageComposer>().Compose(session, theGroup, session.Info);
            }
        }

        #endregion Methods
    }
}