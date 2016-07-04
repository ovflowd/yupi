using System;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Messages.Support
{
	public class ModerationMuteUserMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_mute"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();
			GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

			clientByUserId.GetHabbo().Mute();
			clientByUserId.SendNotif(message);
		}
	}
}

