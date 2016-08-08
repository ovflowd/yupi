using System;
using Yupi.Util;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;



namespace Yupi.Messages.User
{
	public class LoadUserProfileMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;

		public LoadUserProfileMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int userId = message.GetInteger();
			message.GetBool(); // TODO Unused

			UserInfo user = UserRepository.FindBy(userId);

			if (user == null)
			{
				return;
			}

			router.GetComposer<UserProfileMessageComposer> ().Compose (session, user, session.UserData.Info);
			router.GetComposer<UserBadgesMessageComposer> ().Compose (session, user);
		}
	}
}

