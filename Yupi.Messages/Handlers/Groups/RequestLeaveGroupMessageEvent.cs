namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RequestLeaveGroupMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;

        #endregion Fields

        #region Constructors

        public RequestLeaveGroupMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int userId = request.GetInteger();

            Group group = GroupRepository.FindBy(groupId);

            if (group == null || group.Creator.Id == userId)
                return;

            if (userId == session.Info.Id || group.Admins.Contains(session.Info))
            {
                router.GetComposer<GroupAreYouSureMessageComposer>().Compose(session, userId);
            }
        }

        #endregion Methods
    }
}