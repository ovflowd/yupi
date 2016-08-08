using System;




using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.User
{
	public class GiveRespectMessageEvent : AbstractHandler
	{
		private AchievementManager AchievementManager;

		public GiveRespectMessageEvent ()
		{
			AchievementManager = DependencyFactory.Resolve<AchievementManager>();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Room room = session.UserData.Room;

			// TODO Should lock respect points
			if (room == null || session.UserData.Info.Respect.DailyRespectPoints <= 0)
				return;

			int userId = message.GetInteger ();

			if (userId == session.UserData.Info.Id) {
				return;
			}

			UserEntity roomUserByHabbo = room.GetEntity(userId) as UserEntity;

			if (roomUserByHabbo == null)
				return;

			AchievementManager.ProgressUserAchievement (session.UserData, "ACH_RespectGiven", 1);
			AchievementManager.ProgressUserAchievement (roomUserByHabbo.User, "ACH_RespectEarned", 1);

			session.UserData.Info.Respect.DailyRespectPoints--;
			roomUserByHabbo.User.Info.Respect.Respect++;

			router.GetComposer<GiveRespectsMessageComposer> ().Compose (room, roomUserByHabbo.Id, roomUserByHabbo.UserInfo.Respect.Respect);
			router.GetComposer<RoomUserActionMessageComposer> ().Compose (room, roomUserByHabbo.Id);
		}
	}
}

