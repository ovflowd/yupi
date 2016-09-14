namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public abstract class LoadInventoryMessageComposer : AbstractComposer<Inventory>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Inventory inventory)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}