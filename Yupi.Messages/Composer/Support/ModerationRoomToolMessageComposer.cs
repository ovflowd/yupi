using System;

using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Support
{
	public class ModerationRoomToolMessageComposer : AbstractComposer<Room>
	{
		// TODO Refactor
		public override void Compose (Yupi.Protocol.ISender session, Room room, uint roomId)
		{
			RoomData data;

			if (room == null) {
				data = Yupi.GetGame ().GetRoomManager ().GenerateNullableRoomData (roomId);
			} else {
				data = room.RoomData;
			}

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(data.Id);
				message.AppendInteger(data.UsersNow);

				if (room != null)
					message.AppendBool(room.GetRoomUserManager().GetRoomUserByHabbo(data.Owner) != null);
				else
					message.AppendBool(false);

				message.AppendInteger(room?.RoomData.OwnerId ?? 0);
				message.AppendString(data.Owner);
				message.AppendBool(room != null);
				message.AppendString(data.Name);
				message.AppendString(data.Description);
				message.AppendInteger(data.TagCount);

				foreach (string current in data.Tags)
					message.AppendString(current);

				message.AppendBool(false);

				session.Send (message);
			}
		}
	}
}

