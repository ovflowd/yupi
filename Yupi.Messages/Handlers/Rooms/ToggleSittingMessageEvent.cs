using System;


namespace Yupi.Messages.Rooms
{
	public class ToggleSittingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			RoomUser user = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);

			if (user == null)
				return;

			if (user.Statusses.ContainsKey("lay") || user.IsLyingDown || user.RidingHorse || user.IsWalking)
				return;

			if (user.RotBody%2 != 0)
				user.RotBody--;

			user.Z = session.GetHabbo().CurrentRoom.GetGameMap().SqAbsoluteHeight(user.X, user.Y);

			if (!user.Statusses.ContainsKey("sit"))
			{
				user.Statusses.Add("sit", "0.55");
				user.UpdateNeeded = true;
				user.IsSitting = true;
			}

			// TODO Shouldn't this TOGGLE?
		}
	}
}

