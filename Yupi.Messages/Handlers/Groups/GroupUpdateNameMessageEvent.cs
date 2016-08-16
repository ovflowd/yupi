using System;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class GroupUpdateNameMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;

		public GroupUpdateNameMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
		}

		// TODO Refactor
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();
			string name = request.GetString();
			string description = request.GetString();

			Group theGroup = GroupRepository.FindBy(groupId);

			if (theGroup?.Creator != session.UserData.Info)
				return;

			theGroup.Name = name;
			theGroup.Description = description;

			GroupRepository.Save (theGroup);

			if (session.UserData.Room != null) {
				session.UserData.Room.Router.GetComposer<GroupDataMessageComposer> ().Compose (session.UserData.Room, theGroup, session.UserData.Info);
			} else {
				router.GetComposer<GroupDataMessageComposer> ().Compose (session, theGroup, session.UserData.Info);
			}
		}
	}
}

