using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;



namespace Yupi.Messages.Groups
{
	public class SetFavoriteGroupMessageEvent : AbstractHandler
	{
		private IRepository<Group> GroupRepository;
		private IRepository<UserInfo> UserRepository;

		public SetFavoriteGroupMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<IRepository<Group>> ();
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();

			Group theGroup = GroupRepository.FindBy(groupId);

			if (theGroup == null)
				return;

			// TODO Refactor group!
			if (!theGroup.Members.Contains(session.Info))
				return;

			session.Info.FavouriteGroup = theGroup;

			UserRepository.Save (session.Info);

			router.GetComposer<GroupDataMessageComposer> ().Compose (session, theGroup, session.Info);

			// TODO Is this required (see below!)
			router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, session.Info.Id);

			if (session.Room != null && !session.Room.GroupsInRoom.Contains(theGroup))
			{
				session.Room.GroupsInRoom.Add(theGroup);

				session.Room.EachUser ((entitySession) => {
					entitySession.Router.GetComposer<RoomGroupMessageComposer> ().Compose (entitySession, session.Room.GroupsInRoom);
				});

			}

			router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, theGroup, 0);
		}
	}
}

