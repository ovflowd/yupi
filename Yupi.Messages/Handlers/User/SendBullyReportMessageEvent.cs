using System;
using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Messages.User;
using System.Collections.Generic;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Messages.Support;
using Yupi.Protocol;
using Yupi.Controller;

namespace Yupi.Messages.Handlers.User
{
	public class SendBullyReportMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int reportedId = message.GetInteger();

			UserInfo reportedUser = DependencyFactory.Resolve<Repository<UserInfo>> ().FindBy(reportedId);

			SupportTicket ticket = new SupportTicket () {
				Category = 104,
				Type = 9,
				ReportedUser = reportedUser
			};
					
			DependencyFactory.Resolve<ModerationTool>().Tickets.Add(ticket);

			router.GetComposer<BullyReportSentMessageComposer> ().Compose (session);
			router.GetComposer<ModerationToolIssueMessageComposer> ().Compose (DependencyFactory.Resolve<StaffSender>(), ticket);  
		}
	}
}

