using System;
using Yupi.Emulator.Game.Support;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.Support
{
	public class LoadModerationToolMessageComposer : AbstractComposer<ModerationTool, Habbo>
	{
		public override void Compose (Yupi.Protocol.ISender session, ModerationTool tool, Habbo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tool.Tickets.Count);

				foreach (SupportTicket current in tool.Tickets)
					current.Serialize(message);

				message.AppendInteger(tool.UserMessagePresets.Count);

				foreach (string current2 in tool.UserMessagePresets)
					message.AppendString(current2);

				IEnumerable<ModerationTemplate> enumerable =
					(from x in tool.ModerationTemplates.Values where x.Category == -1 select x).ToArray();

				message.AppendInteger(enumerable.Count());
				using (IEnumerator<ModerationTemplate> enumerator3 = enumerable.GetEnumerator())
				{
					bool first = true;

					while (enumerator3.MoveNext())
					{
						ModerationTemplate template = enumerator3.Current;
						IEnumerable<ModerationTemplate> enumerable2 =
							(from x in tool.ModerationTemplates.Values where x.Category == (long) (ulong) template.Id select x)
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
	}
}

