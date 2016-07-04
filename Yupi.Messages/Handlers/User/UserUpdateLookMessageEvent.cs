using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.User
{
	public class UserUpdateLookMessageEvent : AbstractHandler
	{
		public override void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			string gender = message.GetString();
			string look = message.GetString();

			look = Yupi.FilterFigure(look);

			session.GetHabbo().Look = look; 
			// TODO Validate gender
			session.GetHabbo().Gender = gender;
			// TODO Refactor
			using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
			{
				queryReactor.SetQuery(
					$"UPDATE users SET look = @look, gender = @gender WHERE id = {Session.GetHabbo().Id}");
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

			router.GetComposer<UpdateAvatarAspectMessageComposer> ().Compose (session, session.GetHabbo());
			router.GetComposer<UpdateUserDataMessageComposer> ().Compose (currentRoom, session.GetHabbo (), roomUserByHabbo.VirtualId);
		
			if (session.GetHabbo().GetMessenger() != null)
				session.GetHabbo().GetMessenger().OnStatusChanged(true);
		}
	}
}

