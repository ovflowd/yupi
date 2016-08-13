using System;

using Yupi.Messages.User;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;

namespace Yupi.Messages.Messenger
{
	public class FollowFriendMessageEvent : AbstractHandler
	{
		private ClientManager ClientManager;

		public FollowFriendMessageEvent ()
		{
			ClientManager = DependencyFactory.Resolve<ClientManager>();
		}

		// TODO Refactor
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			int userId = request.GetInteger();

			Relationship relationship = session.UserData.Info.Relationships.FindByUser (userId);

			if (relationship == null) {
				router.GetComposer<FollowFriendErrorMessageComposer> ().Compose (session, 0);
			} else {

				var friendSession = ClientManager.GetByUserId (userId);

				if (friendSession == null || friendSession.UserData.Room == null) {
					router.GetComposer<FollowFriendErrorMessageComposer> ().Compose (session, 2);
				} else {
					router.GetComposer<RoomForwardMessageComposer> ().Compose (session, friendSession.UserData.Room.Data.Id);
				}
			}
		}
	}
}

