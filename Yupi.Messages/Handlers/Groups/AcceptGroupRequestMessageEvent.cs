using System;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
    public class AcceptGroupRequestMessageEvent : AbstractHandler
    {
        private readonly IRepository<Group> GroupRepository;
        private readonly IRepository<UserInfo> UserRepository;

        public AcceptGroupRequestMessageEvent()
        {
            GroupRepository = DependencyFactory.Resolve<IRepository<Group>>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var groupId = request.GetInteger();
            var userId = request.GetInteger();

            var group = GroupRepository.FindBy(groupId);
            var user = UserRepository.FindBy(userId);

            if ((group == null) || !group.Admins.Contains(session.Info) || !group.Requests.Contains(user)) return;
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
    }
}