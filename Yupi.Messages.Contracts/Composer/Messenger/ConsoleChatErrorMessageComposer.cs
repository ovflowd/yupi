namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class ConsoleChatErrorMessageComposer : AbstractComposer<int, uint>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int errorId, uint conversationId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}