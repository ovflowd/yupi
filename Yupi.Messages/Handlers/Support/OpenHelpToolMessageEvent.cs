using System.Linq;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class OpenHelpToolMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var openTickets = session.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);
            router.GetComposer<OpenHelpToolMessageComposer>().Compose(session, openTickets.ToList());
        }
    }
}