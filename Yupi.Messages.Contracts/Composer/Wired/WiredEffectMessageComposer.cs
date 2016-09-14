namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class WiredEffectMessageComposer : AbstractComposer<FloorItem, string, int, List<FloorItem>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item, string extraInfo, int delay,
            List<FloorItem> list = null)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}