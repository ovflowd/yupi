using System;

namespace Yupi.Messages.Support
{
	public class ModerationToolPickIssueMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().HasFuse("fuse_mod"))
				return;

			message.GetInteger(); // TODO Unused
			uint ticketId = message.GetUInt32();

			Yupi.GetGame().GetModerationTool().PickTicket(session, ticketId);
		}
	}
}

