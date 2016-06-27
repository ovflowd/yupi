using System;
using Yupi.Emulator.Game.Rooms.Data.Models;

namespace Yupi.Messages.Support
{
	public class ModerationToolRoomToolMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_mod"))
				return;

			uint roomId = message.GetUInt32();

			Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

			ModerationTool.SerializeRoomTool (data);
		}
	}
}

