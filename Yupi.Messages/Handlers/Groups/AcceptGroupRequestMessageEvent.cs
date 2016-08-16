using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;



namespace Yupi.Messages.Groups
{
	public class AcceptGroupRequestMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;
		private Repository<UserInfo> UserRepository;

		public AcceptGroupRequestMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();
			int userId = request.GetInteger();

			Group group = GroupRepository.FindBy(groupId);
			UserInfo user = UserRepository.FindBy (userId);

			if (group == null || !group.Admins.Contains(session.UserData.Info) || !group.Requests.Contains(user)) {
				return;
			}
			throw new NotImplementedException ();
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

