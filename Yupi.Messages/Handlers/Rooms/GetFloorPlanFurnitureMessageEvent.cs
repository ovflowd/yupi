using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class GetFloorPlanFurnitureMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Room room = session.GetHabbo().CurrentRoom;

            if (room != null) {
                router.GetComposer<GetFloorPlanUsedCoordsMessageComposer> ().Compose (session, 
                    room.GetGameMap ().CoordinatedItems.Keys.OfType<Point> ().ToArray ());
            }
            */
            throw new NotImplementedException();
        }
    }
}