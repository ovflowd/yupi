namespace Yupi.Messages.Contracts
{
    using System.Collections.Specialized;

    using Yupi.Protocol.Buffers;

    public abstract class BotInventoryMessageComposer : AbstractComposer<HybridDictionary>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, HybridDictionary items)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}