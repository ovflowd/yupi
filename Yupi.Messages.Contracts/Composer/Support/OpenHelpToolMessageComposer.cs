namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class OpenHelpToolMessageComposer : AbstractComposer<IList<SupportTicket>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<SupportTicket> tickets)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}