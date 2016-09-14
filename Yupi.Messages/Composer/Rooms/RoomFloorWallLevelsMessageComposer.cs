namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class RoomFloorWallLevelsMessageComposer : Yupi.Messages.Contracts.RoomFloorWallLevelsMessageComposer
    {
        #region Methods

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

        #endregion Methods
    }
}