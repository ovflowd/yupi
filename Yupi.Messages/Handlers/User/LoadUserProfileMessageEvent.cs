using System;
using Yupi.Util;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.User
{
	public class LoadUserProfileMessageEvent : AbstractHandler
	{
		private IRepository<UserInfo> UserRepository;

		public LoadUserProfileMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int userId = message.GetInteger();
			message.GetBool(); // TODO Unused (never set to false in client?)

			UserInfo user = UserRepository.FindBy(userId);

			if (user == null)
			{
				return;
			}

			router.GetComposer<UserProfileMessageComposer> ().Compose (session, user, session.Info);
			router.GetComposer<UserBadgesMessageComposer> ().Compose (session, user);
		}
	}
}

