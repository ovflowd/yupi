using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolUserChatlogMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().HasFuse("fuse_chatlogs"))
				return;

			uint userId = message.GetUInt32 ();

			router.GetComposer<ModerationToolUserChatlogMessageComposer> ().Compose (session, userId);
		}
	}
}

