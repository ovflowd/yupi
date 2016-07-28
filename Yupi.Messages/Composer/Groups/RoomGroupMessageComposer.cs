using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;


namespace Yupi.Messages.Groups
{
	public class RoomGroupMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(room.LoadedGroups.Count);

				foreach (KeyValuePair<uint, string> current in room.LoadedGroups)
				{
					message.AppendInteger(current.Key);
					message.AppendString(current.Value);
				}

				room.Send (message);
			}
		}
	}
}

