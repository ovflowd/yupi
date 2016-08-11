using System;
using Yupi.Model;
using Yupi.Model.Repository;
using Yupi.Model.Domain;
using System.Linq;

namespace Yupi.Messages.Support
{
	public class OpenHelpToolMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			var openTickets = session.UserData.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);
			router.GetComposer<OpenHelpToolMessageComposer> ().Compose (session, openTickets.ToList());
		}
	}
}

