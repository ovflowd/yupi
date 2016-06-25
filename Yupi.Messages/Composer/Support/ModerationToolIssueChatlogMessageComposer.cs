using System;
using Yupi.Emulator.Game.Support;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Emulator.Game.Rooms.Chat;
using System.Linq;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.Support
{
	public class ModerationToolIssueChatlogMessageComposer : AbstractComposer
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, SupportTicket ticket, RoomData roomData)
		{
			
			RoomData room = Yupi.GetGame().GetRoomManager().GenerateRoomData(ticket.RoomId);

			if (room == null) {
				return;
			}

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				if (room != null)
				{
					message.AppendInteger(ticket.TicketId);
					message.AppendInteger(ticket.SenderId);
					message.AppendInteger(ticket.ReportedId);
					message.AppendInteger(ticket.RoomId);

					// TODO Hardcoded message
					message.AppendByte(1);
					message.AppendShort(2);
					message.AppendString("roomName");
					message.AppendByte(2);
					message.AppendString(ticket.RoomName);
					message.AppendString("roomId");
					message.AppendByte(1);
					message.AppendInteger(ticket.RoomId);

					List<Chatlog> tempChatlogs =
						room.RoomChat.Reverse().Skip(Math.Max(0, room.RoomChat.Count() - 60)).Take(60).ToList();

					message.AppendShort(tempChatlogs.Count);

					foreach (Chatlog chatLog in tempChatlogs) {
						Habbo habbo = Yupi.GetHabboById(chatLog.UserId);

						message.AppendInteger(Yupi.DifferenceInMilliSeconds(chatLog.TimeStamp, DateTime.Now));
						message.AppendInteger(chatLog.UserId);
						message.AppendString(habbo == null ? "*User not found*" : habbo.UserName);
						message.AppendString(chatLog.Message);
						message.AppendBool(chatLog.GlobalMessage);
					}

					session.Send (message);
				}
			}
		}
	}
}

