using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;


namespace Yupi.Messages.Groups
{
	public class GetGroupForumDataMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;

		public GetGroupForumDataMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger ();

			Group theGroup = GroupRepository.FindBy (groupId);

			if (theGroup != null) {
				router.GetComposer<GroupForumDataMessageComposer> ().Compose (session, theGroup, session.Info);
			}
		}
	}
}

