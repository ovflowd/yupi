using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class ModerationToolIssueMessageComposer : AbstractComposer<SupportTicket>
    {
        public override void Compose(ISender session, SupportTicket ticket)
        {
            // Do nothing by default.
        }
    }
}