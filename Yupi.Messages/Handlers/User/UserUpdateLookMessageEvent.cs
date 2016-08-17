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
		private IRepository<UserInfo> UserRepository;
		private AchievementManager AchievementManager;
		private MessengerController MessengerController;

		public UserUpdateLookMessageEvent ()
		{
			UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>> ();
			AchievementManager = DependencyFactory.Resolve<AchievementManager> ();
			MessengerController = DependencyFactory.Resolve<MessengerController> ();
		}

		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string gender = message.GetString();
			string look = message.GetString();

			// TODO Validate gender & look
			session.Info.Look = look;
            session.Info.Gender = gender;
			UserRepository.Save (session.Info);

			AchievementManager.ProgressUserAchievement(session, "ACH_AvatarLooks", 1);

			if (session.Room == null)
				return;

			router.GetComposer<UpdateAvatarAspectMessageComposer> ().Compose (session, session.Info);
			router.GetComposer<UpdateUserDataMessageComposer> ().Compose (session.Room, session.Info, session.RoomEntity.Id);

			MessengerController.UpdateUser (session.Info);
		}
	}
}

