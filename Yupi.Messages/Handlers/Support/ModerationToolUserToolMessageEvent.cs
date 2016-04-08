using System;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.Support
{
	public class ModerationToolUserToolMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			// TODO Rewrite rights management to prevent usage of strings...
			if (session.GetHabbo().HasFuse("fuse_mod"))
			{
				uint userId = message.GetUInt32();
				router.GetComposer<ModerationToolUserToolMessageComposer> ().Compose (session, userId);
			}
		}
	}
}

