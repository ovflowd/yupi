using System;


namespace Yupi.Messages.Support
{
	public class ModerationToolPerformRoomActionMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			if (!session.GetHabbo().HasFuse("fuse_mod"))
				return;

			uint roomId = message.GetUInt32();

			// TODO Refactor (shoud be enum)
			bool lockRoom = message.GetIntegerAsBool();
			bool inappropriateRoom = message.GetIntegerAsBool();
			bool kickUsers = message.GetIntegerAsBool();

			ModerationTool.PerformRoomAction(session, roomId, kickUsers, lockRoom, inappropriateRoom);
		}
	}
}

