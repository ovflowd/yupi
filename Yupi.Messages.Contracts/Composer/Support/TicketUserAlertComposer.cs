using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class TicketUserAlertComposer : AbstractComposer<TicketUserAlertComposer.Status, SupportTicket>
    {
        public enum Status
        {
            OK = 0,
            HAS_PENDING = 1,
            PREVIOUS_ABUSIVE = 2
        }

        public override void Compose(ISender session, Status status, SupportTicket ticket = null)
        {
            // Do nothing by default.
        }
    }
}