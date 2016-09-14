using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Util;

namespace Yupi.Messages.Support
{
    public class TicketUserAlertComposer : Contracts.TicketUserAlertComposer
    {
        public override void Compose(ISender session, Status status, SupportTicket ticket = null)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) status);

                if (status == Status.HAS_PENDING)
                {
                    Debug.Assert(ticket != null);

                    message.AppendString(ticket.Id.ToString());
                    message.AppendString(ticket.CreatedAt.ToUnix().ToString());
                    message.AppendString(ticket.Message);
                }
                session.Send(message);
            }
        }
    }
}