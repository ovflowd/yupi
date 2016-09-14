namespace Yupi.Messages.Rooms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Numerics;

    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public class UpdateFurniStackMapMessageComposer : Yupi.Messages.Contracts.UpdateFurniStackMapMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<Vector3> affectedTiles, RoomData room)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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

        #endregion Methods
    }
}