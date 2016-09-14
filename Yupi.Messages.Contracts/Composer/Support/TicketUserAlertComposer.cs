namespace Yupi.Messages.Contracts
{
    using System.Diagnostics;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class TicketUserAlertComposer : AbstractComposer<TicketUserAlertComposer.Status, SupportTicket>
    {
        #region Enumerations

        public enum Status
        {
            OK = 0,
            HAS_PENDING = 1,
            PREVIOUS_ABUSIVE = 2
        }

        #endregion Enumerations

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Status status, SupportTicket ticket = null)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}