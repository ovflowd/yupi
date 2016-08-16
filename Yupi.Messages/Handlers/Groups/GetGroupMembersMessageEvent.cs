using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Groups
{
	public class GetGroupMembersMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;

		public GetGroupMembersMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();
			int page = request.GetInteger();
			string searchVal = request.GetString();
			uint reqType = request.GetUInt32();

			Group group = GroupRepository.FindBy (groupId);

			if (group == null) {
				return;
			}

			router.GetComposer<GroupMembersMessageComposer> ().Compose (session, group, reqType, session.UserData.Info, searchVal, page);
		}
	}
}

