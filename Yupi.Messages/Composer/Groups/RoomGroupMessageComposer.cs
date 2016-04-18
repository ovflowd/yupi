using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Messages.Groups
{
	public class RoomGroupMessageComposer : AbstractComposer
	{
		public override void Compose (Room room, Dictionary<uint, string> loadedGroups)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(loadedGroups.Count);

				foreach (KeyValuePair<uint, string> current in loadedGroups)
				{
					message.AppendInteger(current.Key);
					message.AppendString(current.Value);
				}

				room.Send (message);
			}
		}
	}
}

