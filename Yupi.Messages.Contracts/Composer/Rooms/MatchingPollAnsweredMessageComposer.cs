namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class MatchingPollAnsweredMessageComposer : AbstractComposer<uint, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint userId, string text)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}