using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;
using System.Numerics;

namespace Yupi.Messages.Rooms
{
    public class SetFloorPlanDoorMessageComposer : Yupi.Messages.Contracts.SetFloorPlanDoorMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, Vector3 doorPos, int direction)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) doorPos.X);
                message.AppendInteger((int) doorPos.Y);
                message.AppendInteger(direction);
                session.Send(message);
            }
        }
    }
}