namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class WiredTriggerMessageComposer : AbstractComposer
    {
        #region Methods

        public virtual void Compose(Yupi.Protocol.ISender session, FloorItem item, List<FloorItem> items, int delay,
            string extraInfo, int unknown)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}