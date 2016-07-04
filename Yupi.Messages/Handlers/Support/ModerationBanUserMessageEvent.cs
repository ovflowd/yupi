using System;
using Yupi.Emulator.Game.Support;

namespace Yupi.Messages.Support
{
	public class ModerationBanUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_ban"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();
			int duration = request.GetInteger()*3600; // TODO Should be calculated later?

			ModerationTool.BanUser(session, userId, duration, message);
		}
	}
}

