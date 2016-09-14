namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomFloorItemsMessageComposer : AbstractComposer<RoomData, IReadOnlyDictionary<uint, FloorItem>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData data,
            IReadOnlyDictionary<uint, FloorItem> items)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}