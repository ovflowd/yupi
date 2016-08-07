using System;

using Yupi.Messages.User;

namespace Yupi.Messages.Messenger
{
	public class FollowFriendMessageEvent : AbstractHandler
	{
		// TODO Refactor
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			uint userId = request.GetUInt32();
			GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

			if (clientByUserId == null || clientByUserId.GetHabbo() == null) 
				return;
			
			if (clientByUserId.GetHabbo ().GetMessenger () == null || clientByUserId.GetHabbo ().CurrentRoom == null) {
				if (session.GetHabbo ().GetMessenger () == null)
					return;

				router.GetComposer<FollowFriendErrorMessageComposer> ().Compose (session, 2);
			
				session.GetHabbo ().GetMessenger ().UpdateFriend (userId, clientByUserId, true);

			} else if (session.GetHabbo ().Rank < 4 && session.GetHabbo ().GetMessenger () != null &&
			           !session.GetHabbo ().GetMessenger ().FriendshipExists (userId)) {
				router.GetComposer<FollowFriendErrorMessageComposer> ().Compose (session, 0);

			} else {
				router.GetComposer<RoomForwardMessageComposer> ().Compose (session, clientByUserId.GetHabbo ().CurrentRoom.RoomId);
			}
		}
	}
}

