namespace Yupi.Messages.Rooms
{
    using System;
    using System.Numerics;

    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public class SetFloorPlanDoorMessageComposer : Yupi.Messages.Contracts.SetFloorPlanDoorMessageComposer
    {
        #region Methods

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

        #endregion Methods
    }
}