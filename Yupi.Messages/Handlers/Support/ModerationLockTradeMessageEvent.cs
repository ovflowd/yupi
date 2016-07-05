using System;


namespace Yupi.Messages.Support
{
	public class ModerationLockTradeMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_lock_trade"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();
			int length = request.GetInteger()*3600;

			ModerationTool.LockTrade(session, userId, message, length);
		}
	}
}

