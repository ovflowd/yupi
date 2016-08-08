using System;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.User
{
	public class UserUpdateMottoMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;
		private AchievementManager AchievementManager;

		public UserUpdateMottoMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string motto = message.GetString ();

			// TODO Filter!
			session.UserData.Info.Motto = motto;

			UserRepository.Save (session.UserData.Info);

			router.GetComposer<UpdateUserDataMessageComposer> ().Compose (session.UserData.Room, session.UserData.Info, session.UserData.RoomEntity.Id);

			AchievementManager.ProgressUserAchievement(session.UserData, "ACH_Motto", 1);
		}
	}
}

