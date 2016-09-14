using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class ModerationToolIssueChatlogMessageComposer : AbstractComposer<SupportTicket>
    {
        public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
        {
            // Do nothing by default.
        }
    }
}