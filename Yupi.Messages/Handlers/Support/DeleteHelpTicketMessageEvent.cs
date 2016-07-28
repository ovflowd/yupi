using System;

namespace Yupi.Messages.Support
{
	public class DeleteHelpTicketMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!Yupi.GetGame().GetModerationTool().UsersHasPendingTicket(session.GetHabbo().Id))
				return;

			// TODO Might want to replace with TryDelete
			Yupi.GetGame().GetModerationTool().DeletePendingTicketForUser(session.GetHabbo().Id);

			router.GetComposer<OpenHelpToolMessageComposer> ().Compose (session, session.GetHabbo());
		}
	}
}

