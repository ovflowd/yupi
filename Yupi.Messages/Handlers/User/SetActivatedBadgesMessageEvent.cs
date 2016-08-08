using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;



namespace Yupi.Messages.User
{
	public class SetActivatedBadgesMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public SetActivatedBadgesMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			session.UserData.Info.Badges.ResetSlots();

			for (int i = 0; i < 5; i++)
			{
				int slot = message.GetInteger();
				string code = message.GetString();

				Badge badge = session.UserData.Info.Badges.GetBadge (code);

				if (badge == default(Badge) || slot < 1 || slot > 5) {
					continue;
				}

				badge.Slot = slot;
			}

			UserRepository.Save (session.UserData.Info);

			router.GetComposer<UserBadgesMessageComposer> ().Compose (session, session.UserData.Info);
		}
	}
}

