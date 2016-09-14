using System;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
    public class UpdateUserStatusMessageComposer : AbstractComposer<IList<RoomEntity>>
    {
        public override void Compose(Yupi.Protocol.ISender session, IList<RoomEntity> entities)
        {
        }
    }
}