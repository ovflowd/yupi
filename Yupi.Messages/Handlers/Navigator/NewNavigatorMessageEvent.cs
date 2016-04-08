using System;

namespace Yupi.Messages.Navigator
{
	public class NewNavigatorMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (session == null)
				return;

			Yupi.GetGame().GetNavigator().InitializeNavigator(session);
		}
	}
}

