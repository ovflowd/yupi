namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class NewInventoryObjectMessageComposer : AbstractComposer<BaseItem, List<Item>>
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, int itemId)
        {
            // TODO Remove?
        }

        public override void Compose(Yupi.Protocol.ISender session, BaseItem item, List<Item> list)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}