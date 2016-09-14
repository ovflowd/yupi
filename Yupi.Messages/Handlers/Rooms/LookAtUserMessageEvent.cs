using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class LookAtUserMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var x = request.GetInteger();
            var y = request.GetInteger();

            if (session.RoomEntity == null) return;

            Vector3 target = new Vector3(x, y, 0);

            if (session.RoomEntity.Position == target) return;

            int rotation = session.RoomEntity.Position.CalculateRotation(target);

            session.RoomEntity.SetRotation(rotation);

            session.RoomEntity.Wake();

            // TODO Implement Horse
            /*
            if (!roomUserByHabbo.RidingHorse)
                return;

            RoomUser roomUserByVirtualId = session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByVirtualId(Convert.ToInt32(roomUserByHabbo.HorseId));

            roomUserByVirtualId.SetRot(rotation, false);
            roomUserByVirtualId.UpdateNeeded = true;
            */
        }
    }
}