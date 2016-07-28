using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolCloseIssueMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
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

