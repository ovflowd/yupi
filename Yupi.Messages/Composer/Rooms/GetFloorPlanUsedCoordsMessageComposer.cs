using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class GetFloorPlanUsedCoordsMessageComposer : Contracts.GetFloorPlanUsedCoordsMessageComposer
    {
        public override void Compose(ISender session, Point[] coords)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(coords.Length);

                foreach (Point point in coords)
                {
                    message.AppendInteger(point.X);
                    message.AppendInteger(point.Y);
                }

                session.Send(message);
            }
        }
    }
}