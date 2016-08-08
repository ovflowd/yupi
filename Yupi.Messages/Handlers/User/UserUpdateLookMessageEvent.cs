using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;
using Yupi.Messages.Contracts;

namespace Yupi.Messages.User
{
	public class UserUpdateLookMessageEvent : AbstractHandler
	{
		private Repository<UserInfo> UserRepository;
		private AchievementManager AchievementManager;
		private MessengerController MessengerController;

		public UserUpdateLookMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<Repository<UserInfo>> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
			MessengerController = DependencyFactory.Resolve<MessengerController> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string gender = message.GetString();
			string look = message.GetString();

			// TODO Validate gender & look
			session.UserData.Info.Look = look;
            session.UserData.Info.Gender = gender;
			UserRepository.Save (session.UserData.Info);

			AchievementManager.ProgressUserAchievement(session.UserData, "ACH_AvatarLooks", 1);

			if (session.UserData.Room == null)
				return;

			router.GetComposer<UpdateAvatarAspectMessageComposer> ().Compose (session, session.UserData.Info);
			router.GetComposer<UpdateUserDataMessageComposer> ().Compose (session.UserData.Room, session.UserData.Info, session.UserData.RoomEntity.Id);

			MessengerController.UpdateUser (session.UserData.Info);
		}
	}
}

