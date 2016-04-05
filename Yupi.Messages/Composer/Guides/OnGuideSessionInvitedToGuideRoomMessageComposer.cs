using System;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Guides
{
	public class OnGuideSessionInvitedToGuideRoomMessageComposer : AbstractComposer
	{
		public void Compose(GameClient session, int roomId, string roomName) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomId);
				message.AppendString (roomName);
				session.Send (message);
			}
		}
	}
}

