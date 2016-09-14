namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GroupUpdateColoursMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public GroupUpdateColoursMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int color1 = request.GetInteger();
            int color2 = request.GetInteger();

            Group group = GroupRepository.FindBy(groupId);

            if (group?.Creator != session.Info)
                return;

            group.Colour1 = new GroupSymbolColours() {Colour = color1};
            group.Colour2 = new GroupBackGroundColours() {Colour = color2};
            throw new NotImplementedException();
            //router.GetComposer<GroupDataMessageComposer> ().Compose (session.GetHabbo().CurrentRoom, group, session.GetHabbo());
        }

        #endregion Methods
    }
}