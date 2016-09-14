namespace Yupi.Messages.Rooms
{
    using System;
    using System.Drawing;
    using System.Linq;

    public class GetFloorPlanFurnitureMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
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

        #endregion Methods
    }
}