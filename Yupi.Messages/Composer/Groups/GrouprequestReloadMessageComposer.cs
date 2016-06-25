using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Groups
{
	public class GrouprequestReloadMessageComposer : AbstractComposer<int>
	{
		public override void Compose (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, int groupId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (groupId);
				session.Send (message);
			}
		}
	}
}

