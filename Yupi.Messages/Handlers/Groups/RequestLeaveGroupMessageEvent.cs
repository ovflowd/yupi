using System;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class RequestLeaveGroupMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;

		public RequestLeaveGroupMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger ();
			int userId = request.GetInteger ();

			Group group = GroupRepository.FindBy (groupId);

			if (group == null || group.Creator.Id == userId)
				return;

			if (userId == session.Info.Id || group.Admins.Contains(session.Info))
			{
				router.GetComposer<GroupAreYouSureMessageComposer> ().Compose (session, userId);
			}
		}
	}
}

