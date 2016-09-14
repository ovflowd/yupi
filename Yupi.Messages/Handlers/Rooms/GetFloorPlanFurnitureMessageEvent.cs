using System;

using System.Linq;
using System.Drawing;

namespace Yupi.Messages.Rooms
{
	public class GetFloorPlanFurnitureMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Room room = session.GetHabbo().CurrentRoom;

			if (room != null) {
				router.GetComposer<GetFloorPlanUsedCoordsMessageComposer> ().Compose (session, 
					room.GetGameMap ().CoordinatedItems.Keys.OfType<Point> ().ToArray ());
			}
			*/
			throw new NotImplementedException ();
		}
	}
}

