using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class ToggleSittingMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.RoomEntity == null) return;

            if (session.RoomEntity.RotBody%2 != 0) session.RoomEntity.SetRotation(session.RoomEntity.RotBody - 1);

            Vector3 position = session.RoomEntity.Position;
            position.Z = session.Room.HeightMap.GetTileHeight(session.RoomEntity.Position);
            session.RoomEntity.SetPosition(position);

            var status = session.RoomEntity.Status;

            if (status.IsSitting()) status.Stand();
            else status.Sit();
        }
    }
}