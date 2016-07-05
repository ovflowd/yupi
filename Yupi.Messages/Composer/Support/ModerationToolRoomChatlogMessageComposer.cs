using System;
using Yupi.Protocol.Buffers;

using System.Collections.Generic;

using System.Linq;


namespace Yupi.Messages.Support
{
	public class ModerationToolRoomChatlogMessageComposer : AbstractComposer<uint>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint roomId)
		{
			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

			if (room == null || room.RoomData == null) {
				return;
			}

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendByte(1); // TODO Hardcoded
				message.AppendShort(2);
				message.AppendString("roomName");
				message.AppendByte(2);
				message.AppendString(room.RoomData.Name);
				message.AppendString("roomId");
				message.AppendByte(1);
				message.AppendInteger (room.RoomData.Id);

				// TODO Don't reverse. List is a array internally. Reverse will allocate a copy...
				List<Chatlog> tempChatlogs =
					room.RoomData.RoomChat.Reverse()
						.Skip(Math.Max(0, room.RoomData.RoomChat.Count - 60))
						.Take(60)
						.ToList();

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

