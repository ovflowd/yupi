using System;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Support;
using System.Globalization;

namespace Yupi.Messages.Support
{
	public class OpenHelpToolMessageComposer : AbstractComposer<Habbo>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Habbo habbo)
		{
			bool hasTicket = Yupi.GetGame ().GetModerationTool ().UsersHasPendingTicket (habbo.Id);

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (hasTicket ? 1 : 0); // TODO Other values?

				if (hasTicket) {
					SupportTicket ticket = Yupi.GetGame().GetModerationTool().GetPendingTicketForUser(habbo.Id);

					message.AppendString(ticket.TicketId.ToString());
					message.AppendString(ticket.Timestamp.ToString(CultureInfo.InvariantCulture));
					message.AppendString(ticket.Message);
				}

				session.Send ();
			}
		}
	}
}

