using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateFurniStackMapMessageComposer : AbstractComposer<IList<Vector3>, RoomData>
    {
        public override void Compose(ISender session, IList<Vector3> affectedTiles, RoomData room)
        {
            // Do nothing by default.
        }
    }
}