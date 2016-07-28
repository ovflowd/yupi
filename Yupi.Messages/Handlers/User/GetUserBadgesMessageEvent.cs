using System;
using Yupi.Model.Domain;




namespace Yupi.Messages.User
{
	public class GetUserBadgesMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Refactor
			RoomData room = session.UserData.Room;

			uint userId = message.GetUInt32 ();

			RoomEntity roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(userId);

			if (roomUserByHabbo != null && !roomUserByHabbo.IsBot && roomUserByHabbo.GetClient() != null &&
				roomUserByHabbo.GetClient().GetHabbo() != null)
			{
				session.UserData.LastSelectedUser = roomUserByHabbo.UserId;

				router.GetComposer<UserBadgesMessageComposer> ().Compose (session, roomUserByHabbo.GetClient ().GetHabbo ().Id);
			}
		}
	}
}

