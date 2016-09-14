using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OpenHelpToolMessageComposer : AbstractComposer<IList<SupportTicket>>
    {
        public override void Compose(ISender session, IList<SupportTicket> tickets)
        {
            // Do nothing by default.
        }
    }
}