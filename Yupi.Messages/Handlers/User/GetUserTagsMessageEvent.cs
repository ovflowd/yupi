using System;




namespace Yupi.Messages.User
{
	public class GetUserTagsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(session.UserData.CurrentRoomId);

			uint userId = message.GetUInt32 ();

			RoomUser roomUserByHabbo = room?.GetRoomUserManager().GetRoomUserByHabbo(userId);

			if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
				return;

			// TODO Refactor

			router.GetComposer<UserTagsMessageComposer> ().Compose (session, 
				roomUserByHabbo.GetClient ().GetHabbo ().Id, 
				roomUserByHabbo.GetClient ().GetHabbo ().Tags
			);

			if (session != roomUserByHabbo.GetClient())
				return;

			if (session.UserData.Tags.Count >= 5) {
				Yupi.GetGame ()
					.GetAchievementManager ()
					.ProgressUserAchievement (roomUserByHabbo.GetClient (), "ACH_UserTags", 5);
			}
		}
	}
}

