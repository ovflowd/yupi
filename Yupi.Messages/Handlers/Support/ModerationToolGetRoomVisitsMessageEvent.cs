using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolGetRoomVisitsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (session.GetHabbo().HasFuse("fuse_mod"))
			{
				uint userId = message.GetUInt32();

				if (userId > 0) {
					router.GetComposer<ModerationToolRoomVisitsMessageComposer> ().Compose (session, userId);
				}
			}
		}
	}
}

