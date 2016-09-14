using Yupi.Controller;
using Yupi.Model;

namespace Yupi.Rest
{
    public class SSOModule : NancyModule
    {
        private readonly SSOManager SSOManager;

        public SSOModule()
        {
            SSOManager = DependencyFactory.Resolve<SSOManager>();
            Post["/sso/{id:int}"] = parameters =>
            {
                var ticket = SSOManager.GenerateTicket(parameters.id);
                return Response.AsJson(new SSOTicket(ticket));
            };
        }

        private class SSOTicket
        {
            public SSOTicket(string ticket)
            {
                Ticket = ticket;
            }

            public string Ticket { get; set; }
        }
    }
}