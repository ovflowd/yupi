using System;




using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.User
{
	public class GiveRespectMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, ClientMessage message, Yupi.Protocol.IRouter router)
		{
			Room room = session.UserData.Room;

			// TODO Should lock respect points
			if (room == null || session.UserData.Info.DailyRespectPoints <= 0)
				return;

			int userId = message.GetInteger ();

			if (userId == session.UserData.Info.Id) {
				return;
			}

			UserEntity roomUserByHabbo = room.GetEntity(userId) as UserEntity;

			if (roomUserByHabbo == null)
				return;

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_RespectGiven", 1, true);
			Yupi.GetGame()
				.GetAchievementManager()
				.ProgressUserAchievement(roomUserByHabbo.GetClient(), "ACH_RespectEarned", 1, true);

			session.UserData.Info.DailyRespectPoints--;
			roomUserByHabbo.User.Info.Respect++;

			router.GetComposer<GiveRespectsMessageComposer> ().Compose (room, roomUserByHabbo.Id, roomUserByHabbo.UserInfo.Respect);
			router.GetComposer<RoomUserActionMessageComposer> ().Compose (room, roomUserByHabbo.Id);
		}
	}
}

