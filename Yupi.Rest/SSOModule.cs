using System;
using Nancy;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Rest
{
	public class SSOModule : NancyModule
	{
		private SSOManager SSOManager;

		public SSOModule ()
		{
			SSOManager = DependencyFactory.Resolve<SSOManager> ();
			Post["/sso/{id:int}"] = parameters => { 
				string ticket = SSOManager.GenerateTicket(parameters.id);
				return Response.AsJson(new SSOTicket(ticket));
			};
		}

		private class SSOTicket {
			public string Ticket { get; set; }

			public SSOTicket (string ticket)
			{
				this.Ticket = ticket;
			}
			
		}
	}
}

