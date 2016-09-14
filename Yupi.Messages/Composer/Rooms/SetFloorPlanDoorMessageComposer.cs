using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class SetFloorPlanDoorMessageComposer : Contracts.SetFloorPlanDoorMessageComposer
    {
        public override void Compose(ISender session, Vector3 doorPos, int direction)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) doorPos.X);
                message.AppendInteger((int) doorPos.Y);
                message.AppendInteger(direction);
                session.Send(message);
            }
        }
    }
}