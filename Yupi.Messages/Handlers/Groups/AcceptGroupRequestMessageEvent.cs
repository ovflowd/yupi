namespace Yupi.Messages.Groups
{
    using System;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class AcceptGroupRequestMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<Group> GroupRepository;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public AcceptGroupRequestMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int groupId = request.GetInteger();
            int userId = request.GetInteger();

            Group group = GroupRepository.FindBy(groupId);
            UserInfo user = UserRepository.FindBy(userId);

            if (group == null || !group.Admins.Contains(session.Info) || !group.Requests.Contains(user))
            {
                return;
            }
            throw new NotImplementedException();
            /*

            GroupMember memberGroup = group.Requests[userId];

            memberGroup.DateJoin = Yupi.GetUnixTimeStamp();
            group.Members.Add(userId, memberGroup);
            group.Requests.Remove(userId);
            group.Admins.Add(userId, group.Members[userId]);

            router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.GetHabbo());
            router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, 0u, session);
            */
        }

        #endregion Methods
    }
}