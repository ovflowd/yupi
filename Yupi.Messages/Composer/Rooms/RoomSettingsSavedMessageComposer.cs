using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomSettingsSavedMessageComposer : Yupi.Messages.Contracts.RoomSettingsSavedMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int roomId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (roomId);
				session.Send (message);
			}
		}
	}
}

