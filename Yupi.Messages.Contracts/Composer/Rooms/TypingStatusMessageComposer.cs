namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class TypingStatusMessageComposer : AbstractComposer<int, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int virtualId, bool isTyping)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}