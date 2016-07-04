using System;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Rooms
{
	public class ToggleSittingMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
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

