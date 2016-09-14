namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ChatMessageComposer : AbstractComposer<ChatMessage, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, ChatMessage msg, int count = -1)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}