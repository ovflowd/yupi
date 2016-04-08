using System;

namespace Yupi.Messages.Messenger
{
	// TODO Rename?
	public class ConsoleInstantChatMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint toId = request.GetUInt32();
			string text = request.GetString();

			if (session.GetHabbo().GetMessenger() == null || string.IsNullOrWhiteSpace (text))
				return;
			

		    session.GetHabbo ().GetMessenger ().SendInstantMessage (toId, text);
		}
	}
}

