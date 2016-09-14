using System;

using Yupi.Protocol.Buffers;

using System.Globalization;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Util;
using System.Collections.Generic;

namespace Yupi.Messages.Support
{
	public class OpenHelpToolMessageComposer : Yupi.Messages.Contracts.OpenHelpToolMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<SupportTicket> tickets)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (tickets.Count);

				foreach(SupportTicket ticket in tickets) {
					message.AppendString(ticket.Id.ToString());
					message.AppendString(ticket.CreatedAt.ToUnix().ToString());
					message.AppendString(ticket.Message);
				}

				session.Send (message);
			}
		}
	}
}

