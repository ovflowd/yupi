using System;
using Yupi.Emulator.Game.Support;

namespace Yupi.Messages.Support
{
	public class SubmitHelpTicketMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			string content = message.GetString();
			int category = message.GetInteger();
			uint reportedUser = message.GetUInt32();
			uint roomId = message.GetUInt32();

			int messageCount = message.GetInteger();

			string[] chats = new string[messageCount];

			for (int i = 0; i < messageCount; ++i)
			{
				message.GetInteger();

				chats[i] = message.GetString();
			}

			// TODO Refactor
			if (Yupi.GetGame ().GetModerationTool ().UsersHasPendingTicket (session.GetHabbo ().Id)) {
				SupportTicket ticket = Yupi.GetGame ().GetModerationTool ().GetPendingTicketForUser (session.GetHabbo ().Id);
				router.GetComposer<TicketUserAlert> ().Compose (session, TicketUserAlert.Status.HAS_PENDING, ticket);
			} else if (Yupi.GetGame ().GetModerationTool ().UsersHasAbusiveCooldown (session.GetHabbo ().Id)) {
				router.GetComposer<TicketUserAlert> ().Compose (session, TicketUserAlert.Status.PREVIOUS_ABUSIVE);
			} else {
				router.GetComposer<TicketUserAlert> ().Compose (session, TicketUserAlert.Status.OK);
				// TODO Hardcoded value
				Yupi.GetGame().GetModerationTool().SendNewTicket(session, category, 7, reportedUser, message, chats);
			}
		}
	}
}

