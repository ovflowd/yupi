using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SetRoomUserMessageComposer : AbstractComposer<IList<RoomEntity>>
    {
        public override void Compose(ISender room, IList<RoomEntity> users)
        {
            // Do nothing by default.
        }

        public virtual void Compose(ISender room, RoomEntity user)
        {
            // Do nothing by default.
        }
    }
}