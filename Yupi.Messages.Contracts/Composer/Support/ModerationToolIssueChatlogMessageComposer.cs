namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ModerationToolIssueChatlogMessageComposer : AbstractComposer<SupportTicket>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}