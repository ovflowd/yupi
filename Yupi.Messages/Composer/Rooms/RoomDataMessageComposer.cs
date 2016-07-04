using System;
using Yupi.Emulator.Game.Rooms;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomDataMessageComposer : AbstractComposer<Room, bool, bool>
	{
		public override void Compose (Yupi.Protocol.ISender session, Room room, bool show, bool isNotReload)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(show);

				Serialize(message, room.RoomData, true, !isNotReload);

				message.AppendBool(isNotReload);
				message.AppendBool(Yupi.GetGame().GetNavigator() != null && Yupi.GetGame().GetNavigator().GetPublicRoom(room.RoomData.Id) != null);
				message.AppendBool(!isNotReload || session.GetHabbo().HasFuse("fuse_mod"));
				message.AppendBool(room.RoomMuted == true);
				message.AppendInteger(room.RoomData.WhoCanMute);
				message.AppendInteger(room.RoomData,WhoCanKick);
				message.AppendInteger(room.RoomData.WhoCanBan);
				message.AppendBool(room.CheckRights(session, true) == true);
				message.AppendInteger(room.RoomData.ChatType);
				message.AppendInteger(room.RoomData.ChatBalloon);
				message.AppendInteger(room.RoomData.ChatSpeed);
				message.AppendInteger(room.RoomData.ChatMaxDistance);
				message.AppendInteger(room.RoomData.ChatFloodProtection);
				session.Send (message);
			}
		}
	}
}

