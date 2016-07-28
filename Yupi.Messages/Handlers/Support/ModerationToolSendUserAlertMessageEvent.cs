using System;


namespace Yupi.Messages.Support
{
	public class ModerationToolSendUserAlertMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.GetHabbo().HasFuse("fuse_alert"))
				return;

			uint userId = request.GetUInt32();
			string message = request.GetString();

			ModerationTool.AlertUser(session, userId, message, false);
		}
	}
}

