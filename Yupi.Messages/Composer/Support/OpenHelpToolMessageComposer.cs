using System;

using Yupi.Protocol.Buffers;

using System.Globalization;
using Yupi.Model.Domain;

namespace Yupi.Messages.Support
{
	public class OpenHelpToolMessageComposer : AbstractComposer<Habbo>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Habbo habbo)
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

