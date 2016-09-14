using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ModerationToolIssueChatlogMessageComposer : AbstractComposer<SupportTicket>
    {
        public override void Compose(ISender session, SupportTicket ticket)
        {
            // Do nothing by default.
        }
    }
}