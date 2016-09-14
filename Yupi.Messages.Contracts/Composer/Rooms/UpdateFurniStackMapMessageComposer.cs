namespace Yupi.Messages.Contracts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public abstract class UpdateFurniStackMapMessageComposer : AbstractComposer<IList<Vector3>, RoomData>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<Vector3> affectedTiles, RoomData room)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}