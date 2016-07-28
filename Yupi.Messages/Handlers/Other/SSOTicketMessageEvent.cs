using System;

namespace Yupi.Messages.Other
{
	public class SSOTicketMessageEvent : AbstractHandler
	{
		public override bool RequireUser {
			get { 
				return false; 
			}
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string ssoTicket = request.GetString ();

			string banReason;
			// TODO Why would it be useful to display the ban reason on the console?
			if (!session.TryLogin(ssoTicket, out banReason))
				session.Disconnect("Banned from Server.", true);

			session.TimePingedReceived = DateTime.Now;
		}
	}
}

