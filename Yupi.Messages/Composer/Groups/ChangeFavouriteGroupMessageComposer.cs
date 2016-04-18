using System;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Rooms.User;

namespace Yupi.Messages.Groups
{
	public class ChangeFavouriteGroupMessageComposer : AbstractComposer
	{
		// TODO Refactor
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Group group, int virtualId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(virtualId);

				if (group == null) {
					message.AppendInteger (-1);
					message.AppendInteger (-1);
					message.AppendString (string.Empty);
				} else {
					message.AppendInteger (group.Id);
					message.AppendInteger (3); // TODO Hardcoded
					message.AppendString (group.Name);

				}
				session.Send (message);
			}
		}
	}
}

