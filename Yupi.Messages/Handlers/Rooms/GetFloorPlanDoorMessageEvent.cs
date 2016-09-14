using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class GetFloorPlanDoorMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var room = session.Room;
            if (room != null)
                router.GetComposer<SetFloorPlanDoorMessageComposer>().Compose(session,
                    room.Data.Model.Door,
                    room.Data.Model.DoorOrientation);
        }
    }
}