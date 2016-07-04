using System;
using Yupi.Emulator.Game.Rooms.User;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class UpdateUserStatusMessageComposer : AbstractComposer<List<RoomUser>>
	{
		public override void Compose (Yupi.Protocol.ISender session, List<RoomUser> users)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(users.Count);

				foreach (RoomUser current2 in users)
					current2.SerializeStatus(message);
				session.Send (message);
			}
		}

		public void Compose (Yupi.Protocol.ISender session, RoomUser user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				user.SerializeStatus(message);
				session.Send (message);
			}
		}
	}
}

