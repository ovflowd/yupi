using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolCloseIssueMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (!session.GetHabbo ().HasFuse ("fuse_mod")) {
				return;
			}

			int result = message.GetInteger();

			message.GetInteger(); // TODO unused

			uint ticketId = message.GetUInt32();

			if (ticketId <= 0) { // TODO Early validation?
				return; 
			}

			Yupi.GetGame().GetModerationTool().CloseTicket(session, ticketId, result);
		}
	}
}

