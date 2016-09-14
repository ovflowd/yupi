namespace Yupi.Messages.Rooms
{
    using System;
    using System.Drawing;

    using Yupi.Protocol.Buffers;

    public class GetFloorPlanUsedCoordsMessageComposer : Yupi.Messages.Contracts.GetFloorPlanUsedCoordsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Point[] coords)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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

        #endregion Methods
    }
}