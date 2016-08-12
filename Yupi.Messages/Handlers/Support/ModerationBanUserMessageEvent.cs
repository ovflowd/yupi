using System;
using Yupi.Controller;
using Yupi.Model;


namespace Yupi.Messages.Support
{
	public class ModerationBanUserMessageEvent : AbstractHandler
	{
		private ModerationTool ModerationTool;

		public ModerationBanUserMessageEvent ()
		{
			ModerationTool = DependencyFactory.Resolve<ModerationTool>();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			if (!session.UserData.Info.HasPermission("fuse_ban"))
				return;

			int userId = request.GetInteger();
			string reason = request.GetString();
			int hours = request.GetInteger();

			if (ModerationTool.CanBan (session.UserData.Info, userId)) {
				ModerationTool.BanUser (userId, hours, reason);
			}
		}
	}
}

