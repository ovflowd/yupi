using System;



namespace Yupi.Messages.User
{
	public class LoadUserProfileMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint userId = message.GetUInt32();
			message.GetBool(); // TODO Unused

			Habbo habbo = Yupi.GetHabboById(userId);
			if (habbo == null)
			{
				session.SendNotif(Yupi.GetLanguage().GetVar("user_not_found"));
				return;
			}

			router.GetComposer<UserProfileMessageComposer> ().Compose (session, habbo);
			router.GetComposer<UserBadgesMessageComposer> ().Compose (session, habbo.Id);
		}
	}
}

