namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ConsoleChatMessageComposer : AbstractComposer<MessengerMessage>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, MessengerMessage message)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}