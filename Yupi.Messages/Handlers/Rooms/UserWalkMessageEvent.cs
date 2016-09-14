using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class UserWalkMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var targetX = request.GetInteger();
            var targetY = request.GetInteger();

            RoomEntity entity = session.RoomEntity;

            var target = new Vector2(targetX, targetY);

            if ((entity == null) || !entity.CanWalk() || entity.Position.Equals(target)) return;

            entity.Wake();

            entity.Walk(target);

            /* TODO Implement Horse
            if (entity.RidingHorse) {
                RoomUser roomUserByVirtualId = currentRoom.GetRoomUserManager ().GetRoomUserByVirtualId ((int)roomUserByHabbo.HorseId);

                roomUserByVirtualId.MoveTo (targetX, targetY);
            }*/
        }
    }
}