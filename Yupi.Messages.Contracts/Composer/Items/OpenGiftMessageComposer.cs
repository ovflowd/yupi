namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class OpenGiftMessageComposer : AbstractComposer<BaseItem, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, BaseItem item, string text)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}