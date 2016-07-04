using System;
using Yupi.Emulator.Game.Rooms;
using System.Linq;
using System.Drawing;

namespace Yupi.Messages.Rooms
{
	public class GetFloorPlanFurnitureMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = session.GetHabbo().CurrentRoom;

			if (room != null) {
				router.GetComposer<GetFloorPlanUsedCoordsMessageComposer> ().Compose (session, 
					room.GetGameMap ().CoordinatedItems.Keys.OfType<Point> ().ToArray ());
			}
		}
	}
}

