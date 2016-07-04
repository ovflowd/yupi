using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolReleaseIssueMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_mod"))
				return;

			int ticketCount = message.GetInteger();

			for (int i = 0; i < ticketCount; i++) {
				Yupi.GetGame ().GetModerationTool ().ReleaseTicket (session, message.GetUInt32 ());
			}
		}
	}
}

