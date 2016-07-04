using System;

namespace Yupi.Messages.Messenger
{
	public class ConsoleSearchFriendsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			// TODO Can the messenger ever be null?
			if (session.GetHabbo().GetMessenger() == null) 
				return;

			string query = request.GetString ();

			// TODO Refactor
			session.Send(session.GetHabbo().GetMessenger().PerformSearch(query));
		}
	}
}

