using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Other
{
    public class SSOTicketMessageEvent : AbstractHandler
    {
        private readonly SSOManager SSOManager;

        public SSOTicketMessageEvent()
        {
            SSOManager = DependencyFactory.Resolve<SSOManager>();
        }

        public override bool RequireUser
        {
            get { return false; }
        }


        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var ssoTicket = request.GetString();

            SSOManager.TryLogin(session, ssoTicket);
        }
    }
}