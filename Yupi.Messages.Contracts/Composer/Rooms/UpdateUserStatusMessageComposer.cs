using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public class UpdateUserStatusMessageComposer : AbstractComposer<IList<RoomEntity>>
    {
        public override void Compose(ISender session, IList<RoomEntity> entities)
        {
        }
    }
}