using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Groups
{
	public class GetGroupInfoMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;

		public GetGroupInfoMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();
			bool newWindow = request.GetBool();

			Group group = GroupRepository.FindBy (groupId);

			if (group == null)
				return;

			router.GetComposer<GroupDataMessageComposer> ().Compose (session, group, session.UserData.Info, newWindow);
		}
	}
}

