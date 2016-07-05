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

		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
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

