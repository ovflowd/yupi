namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class CatalogPromotionGetRoomsMessageComposer : AbstractComposer<IList<RoomData>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<RoomData> rooms)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}