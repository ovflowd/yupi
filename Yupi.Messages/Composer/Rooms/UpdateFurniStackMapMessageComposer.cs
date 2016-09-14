using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class UpdateFurniStackMapMessageComposer : Contracts.UpdateFurniStackMapMessageComposer
    {
        public override void Compose(ISender session, IList<Vector3> affectedTiles, RoomData room)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendByte((byte) affectedTiles.Count);
                foreach (Vector3 coord in affectedTiles)
                {
                    // TODO What about coord.Z?
                    message.AppendByte((byte) coord.X);
                    message.AppendByte((byte) coord.Y);
                    throw new NotImplementedException();
                    //	message.AppendShort((short) (room.GetGameMap().SqAbsoluteHeight(coord.X, coord.Y)*256));
                }
                session.Send(message);
            }
        }
    }
}