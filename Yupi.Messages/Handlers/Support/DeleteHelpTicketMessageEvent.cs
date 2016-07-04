using System;

namespace Yupi.Messages.Support
{
	public class DeleteHelpTicketMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (!Yupi.GetGame().GetModerationTool().UsersHasPendingTicket(session.GetHabbo().Id))
				return;

			// TODO Might want to replace with TryDelete
			Yupi.GetGame().GetModerationTool().DeletePendingTicketForUser(session.GetHabbo().Id);

			router.GetComposer<OpenHelpToolMessageComposer> ().Compose (session, session.GetHabbo());
		}
	}
}

