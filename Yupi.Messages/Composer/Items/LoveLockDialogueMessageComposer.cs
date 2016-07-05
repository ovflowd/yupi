using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Items
{
	public class LoveLockDialogueMessageComposer : AbstractComposer
	{
		public override void Compose (Yupi.Protocol.ISender user1, Yupi.Emulator.Game.GameClients.Interfaces.GameClient user2, RoomItem loveLock)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(loveLock.Id);
				message.AppendBool(true);

				// TODO use loveLock.InteractingUser
				user1.Send (message);
				user2.Send (message);
			}
		}
	}
}

