using System;



namespace Yupi.Messages.User
{
	public class SetActivatedBadgesMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.GetHabbo().GetBadgeComponent().ResetSlots();

			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
				queryReactor.RunFastQuery(
					$"UPDATE users_badges SET badge_slot = 0 WHERE user_id = {Session.GetHabbo().Id}");

			for (int i = 0; i < 5; i++)
			{
				int slot = message.GetInteger();
				string code = message.GetString();

				if (code.Length == 0)
					continue;

				if (!session.GetHabbo().GetBadgeComponent().HasBadge(code) || slot < 1 || slot > 5)
					return;

				session.GetHabbo().GetBadgeComponent().GetBadge(code).Slot = slot;

				using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
				{
					queryreactor2.SetQuery("UPDATE users_badges SET badge_slot = @slot WHERE badge_id = @badge AND user_id = @user");
					queryreactor2.AddParameter("slot", slot);
					queryreactor2.AddParameter("badge", code);
					queryreactor2.AddParameter ("user", session.GetHabbo ().Id);
					queryreactor2.RunQuery();
				}
			}

			router.GetComposer<UserBadgesMessageComposer> ().Compose (session, session.GetHabbo ().Id);
		}
	}
}

