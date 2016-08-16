using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;



namespace Yupi.Messages.Groups
{
	public class SetFavoriteGroupMessageEvent : AbstractHandler
	{
		private Repository<Group> GroupRepository;
		private Repository<UserInfo> UserRepository;

		public SetFavoriteGroupMessageEvent ()
		{
			GroupRepository = DependencyFactory.Resolve<Repository<Group>> ();
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int groupId = request.GetInteger();

			Group theGroup = GroupRepository.FindBy(groupId);

			if (theGroup == null)
				return;

			// TODO Refactor group!
			if (!theGroup.Members.Contains(session.UserData.Info))
				return;

			session.UserData.Info.FavouriteGroup = theGroup;

			UserRepository.Save (session.UserData.Info);

			router.GetComposer<GroupDataMessageComposer> ().Compose (session, theGroup, session.UserData.Info);

			// TODO Is this required (see below!)
			router.GetComposer<FavouriteGroupMessageComposer> ().Compose (session, session.UserData.Info.Id);

			if (session.UserData.Room != null && !session.UserData.Room.GroupsInRoom.Contains(theGroup))
			{
				session.UserData.Room.GroupsInRoom.Add(theGroup);
				session.UserData.Room.Router.GetComposer<RoomGroupMessageComposer> ().Compose (session.UserData.Room, session.UserData.Room.GroupsInRoom);
			}

			router.GetComposer<ChangeFavouriteGroupMessageComposer> ().Compose (session, theGroup, 0);
		}
	}
}

