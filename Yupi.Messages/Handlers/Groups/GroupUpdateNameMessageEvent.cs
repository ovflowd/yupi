using System;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class GroupUpdateNameMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;

		public GroupUpdateNameMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
		}

		// TODO Refactor
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();
			string name = request.GetString();
			string description = request.GetString();

			Group theGroup = GroupRepository.FindBy(groupId);

			if (theGroup?.Creator != session.Info)
				return;

			theGroup.Name = name;
			theGroup.Description = description;

			GroupRepository.Save (theGroup);

			if (session.Room != null) {
				session.Room.Each ((entitySession) => {
					entitySession.Room.Router.GetComposer<GroupDataMessageComposer> ().Compose (entitySession, theGroup, session.Info);
				});

			} else {
				router.GetComposer<GroupDataMessageComposer> ().Compose (session, theGroup, session.Info);
			}
		}
	}
}

