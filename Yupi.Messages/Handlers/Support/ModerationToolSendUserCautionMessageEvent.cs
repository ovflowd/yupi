using System;


namespace Yupi.Messages.Support
{
	public class ModerationToolSendUserCautionMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().HasFuse("fuse_alert"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();

			ModerationTool.AlertUser(session, userId, message, true);
		}
	}
}

