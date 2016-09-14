using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class RoomFloorWallLevelsMessageComposer : Contracts.RoomFloorWallLevelsMessageComposer
    {
        public override void Compose(ISender session, RoomData data)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendBool(data.HideWall);
                message.AppendInteger(data.WallThickness);
                message.AppendInteger(data.FloorThickness);
                session.Send(message);
            }
        }
    }
}