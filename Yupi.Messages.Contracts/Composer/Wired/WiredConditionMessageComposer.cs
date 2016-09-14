namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class WiredConditionMessageComposer : AbstractComposer<FloorItem, List<FloorItem>, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item, List<FloorItem> list,
            string extraString)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}