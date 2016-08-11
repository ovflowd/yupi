using System;


using Yupi.Protocol.Buffers;
using System.Collections.Generic;

using System.Linq;
using Yupi.Model.Domain;


namespace Yupi.Messages.Support
{
	public class ModerationToolIssueChatlogMessageComposer : Yupi.Messages.Contracts.ModerationToolIssueChatlogMessageComposer
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, SupportTicket ticket)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (ticket.Id);
				message.AppendInteger (ticket.Sender.Id);
				message.AppendInteger (ticket.ReportedUser.Id);
				message.AppendInteger (ticket.Room.Id);

				// TODO Hardcoded message
				message.AppendByte (1);
				message.AppendShort (2);
				message.AppendString ("roomName");
				message.AppendByte (2);
				message.AppendString (ticket.Room.Name);
				message.AppendString ("roomId");
				message.AppendByte (1);
				message.AppendInteger (ticket.Room.Id);

				int count = Math.Min (ticket.Room.Chatlog.Count, 60);
				message.AppendShort((short)count);

				for (int i = 1; i <= count; ++i) {
					ChatlogEntry entry = ticket.Room.Chatlog [ticket.Room.Chatlog.Count - i];
					message.AppendInteger((int)(DateTime.Now - entry.Timestamp).TotalMilliseconds);
					message.AppendInteger(entry.User.Id);
					message.AppendString(entry.User.UserName);
					message.AppendString(entry.Message);
					message.AppendBool(entry.GlobalMessage);
				}

				session.Send (message);

			}
		}
	}
}

