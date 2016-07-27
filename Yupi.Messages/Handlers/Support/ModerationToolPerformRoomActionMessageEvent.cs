using System;


namespace Yupi.Messages.Support
{
	public class ModerationToolPerformRoomActionMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
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

