namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ModerationToolIssueMessageComposer : AbstractComposer<SupportTicket>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}