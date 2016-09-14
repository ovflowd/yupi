namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SuggestPollMessageComposer : AbstractComposer<Poll>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Poll poll)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}