using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Util;

namespace Yupi.Messages.Support
{
    public class OpenHelpToolMessageComposer : Contracts.OpenHelpToolMessageComposer
    {
        public override void Compose(ISender session, IList<SupportTicket> tickets)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(tickets.Count);

                foreach (var ticket in tickets)
                {
                    message.AppendString(ticket.Id.ToString());
                    message.AppendString(ticket.CreatedAt.ToUnix().ToString());
                    message.AppendString(ticket.Message);
                }

                session.Send(message);
            }
        }
    }
}