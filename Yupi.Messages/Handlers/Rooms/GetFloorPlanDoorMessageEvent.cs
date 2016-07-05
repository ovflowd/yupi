using System;


namespace Yupi.Messages.Rooms
{
	public class GetFloorPlanDoorMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = session.GetHabbo().CurrentRoom;

			if (room != null) {
				router.GetComposer<SetFloorPlanDoorMessageComposer> ().Compose (session, 
					room.GetGameMap ().Model.DoorX,
					room.GetGameMap ().Model.DoorY,
					room.GetGameMap ().Model.DoorOrientation);
			}
		}
	}
}

