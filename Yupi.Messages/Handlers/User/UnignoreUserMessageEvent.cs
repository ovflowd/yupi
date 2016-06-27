using System;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.User
{
	public class UnignoreUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			string text = request.GetString();
			Habbo habbo = Yupi.GetGame().GetClientManager().GetClientByUserName(text).GetHabbo();

			if (habbo == null)
				return;

			if (!session.GetHabbo().MutedUsers.Contains(habbo.Id))
				return;

			session.GetHabbo().MutedUsers.Remove(habbo.Id);

			router.GetComposer<UpdateIgnoreStatusMessageComposer> ().Compose (session, UpdateIgnoreStatusMessageComposer.State.LISTEN, username);
		}
	}
}

