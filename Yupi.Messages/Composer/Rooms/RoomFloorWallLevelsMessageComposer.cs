using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
    public class RoomFloorWallLevelsMessageComposer : Yupi.Messages.Contracts.RoomFloorWallLevelsMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, RoomData data)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(data.HideWall);
                message.AppendInteger(data.WallThickness);
                message.AppendInteger(data.FloorThickness);
                session.Send(message);
            }
        }
    }
}