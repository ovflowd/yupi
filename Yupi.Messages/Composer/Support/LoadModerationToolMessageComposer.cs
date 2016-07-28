using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model;

namespace Yupi.Messages.Support
{
	public class LoadModerationToolMessageComposer : AbstractComposer<ModerationTool, Habbo>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, ModerationTool tool, Habbo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tool.Tickets.Count);

				foreach (SupportTicket current in tool.Tickets) {
					SerializeTicket(message);
				}	

				message.AppendInteger(tool.UserMessagePresets.Count);

				foreach (string text in tool.UserMessagePresets) {
					message.AppendString (text);
				}

				// TODO Implement categories correctly... This is a mess...
				IEnumerable<ModerationTemplate> enumerable =
					(from x in tool.Templates where x.Category == -1 select x).ToArray();

				message.AppendInteger(enumerable.Count());
				using (IEnumerator<ModerationTemplate> enumerator3 = enumerable.GetEnumerator())
				{
					bool first = true;

					while (enumerator3.MoveNext())
					{
						ModerationTemplate template = enumerator3.Current;
						IEnumerable<ModerationTemplate> enumerable2 =
							(from x in tool.Templates where x.Category == (long) (ulong) template.Id select x)
								.ToArray();
						message.AppendString(template.CName);
						message.AppendBool(first);
						message.AppendInteger(enumerable2.Count());

						foreach (ModerationTemplate current3 in enumerable2)
						{
							message.AppendString(current3.Caption);
							message.AppendString(current3.BanMessage);
							message.AppendInteger(current3.BanHours);
							message.AppendInteger(current3.AvatarBan);
							message.AppendInteger(current3.Mute);
							message.AppendInteger(current3.TradeLock);
							message.AppendString(current3.WarningMessage);
							message.AppendBool(true);
						}

						first = false;
					}
				}

				message.AppendBool(true); //ticket_queue_button
				message.AppendBool(user.HasFuse("fuse_chatlogs")); //chatlog_button
				message.AppendBool(user.HasFuse("fuse_alert")); //message_button
				message.AppendBool(true); //modaction_but
				message.AppendBool(user.HasFuse("fuse_ban")); //ban_button
				message.AppendBool(true);
				message.AppendBool(user.HasFuse("fuse_kick")); //kick_button

				message.AppendInteger(tool.RoomMessagePresets.Count);

				foreach (string current4 in tool.RoomMessagePresets)
					message.AppendString(current4);

				session.Send (message);
			}
		}

		private void SerializeTicket(ServerMessage message, SupportTicket ticket) {
			message.AppendInteger(ticket.Id);
			message.AppendInteger(ticket.Status);
			message.AppendInteger(ticket.Type); // type (3 or 4 for new style)
			message.AppendInteger(ticket.Category);
			message.AppendInteger((Yupi.GetUnixTimeStamp() - (int) Timestamp)*1000);
			message.AppendInteger(ticket.Score);
			message.AppendInteger(1);
			message.AppendInteger(ticket.Sender.Id);
			message.AppendString(ticket.Sender.UserName);
			message.AppendInteger(ticket.ReportedUser.Id);
			message.AppendString(ticket.ReportedUser.UserName);
			message.AppendInteger(ticket.Staff.Id);
			message.AppendString(ticket.Staff.UserName);
			message.AppendString(ticket.Message);
			message.AppendInteger(0);

			message.AppendInteger(ticket.ReportedChats.Count);

			foreach (string str in ticket.ReportedChats)
			{
				message.AppendString(str);
				message.AppendInteger(-1);
				message.AppendInteger(-1);
			}
		}
	}
}

