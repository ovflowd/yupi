using System;
using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Messages.Other
{
	public class SSOTicketMessageEvent : AbstractHandler
	{
		public override bool RequireUser {
			get { 
				return false; 
			}
		}

		private SSOManager SSOManager;

		public SSOTicketMessageEvent ()
		{
			SSOManager = DependencyFactory.Resolve<SSOManager> ();
		}


		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			string ssoTicket = request.GetString ();

			SSOManager.TryLogin (session, ssoTicket);
		}
	}
}

