using System;
using Yupi.Emulator.Game.Support;
using Yupi.Protocol.Buffers;
using System.Diagnostics;
using System.Globalization;

namespace Yupi.Messages.Support
{
	public class TicketUserAlert : AbstractComposer
	{
		public enum Status {
			OK = 0,
			HAS_PENDING = 1,
			PREVIOUS_ABUSIVE = 2
		}

		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Status status, SupportTicket ticket = null)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(status);

				if (status == Status.HAS_PENDING) {
					Debug.Assert(ticket != null);

					message.AppendString (ticket.TicketId.ToString ());
					message.AppendString (ticket.Timestamp.ToString (CultureInfo.InvariantCulture));
					message.AppendString (ticket.Message);
				}
				session.Send (message);
			}
		}
	}
}

