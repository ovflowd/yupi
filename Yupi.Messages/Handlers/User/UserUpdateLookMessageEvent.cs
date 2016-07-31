using System;





namespace Yupi.Messages.User
{
	public class UserUpdateLookMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			string gender = message.GetString();
			string look = message.GetString();

			look = Yupi.FilterFigure(look);

			session.UserData.Info.Look = look;
            // TODO Validate gender
            session.UserData.Info.Gender = gender;
			// TODO Refactor
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor()) // This going to be changed to NHibernate - Zak
			{
				queryReactor.SetQuery(
					$"UPDATE users SET look = @look, gender = @gender WHERE id = {session.UserDatai}");
				queryReactor.AddParameter("look", look);
				queryReactor.AddParameter("gender", gender);
				queryReactor.RunQuery();
			}

			Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_AvatarLooks", 1);

			if (!Session.GetHabbo().InRoom)
				return;

			Yupi.Messages.Rooms currentRoom = Session.GetHabbo().CurrentRoom;

			RoomUser roomUserByHabbo = currentRoom?.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

			if (roomUserByHabbo == null)
				return;

			router.GetComposer<UpdateAvatarAspectMessageComposer> ().Compose (session, session.UserData.Info);
			router.GetComposer<UpdateUserDataMessageComposer> ().Compose (currentRoom, session.UserData.Info, roomUserByHabbo.VirtualId);
		
			if (session.GetHabbo().GetMessenger() != null)
				session.GetHabbo().GetMessenger().OnStatusChanged(true);
		}
	}
}

