using System;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
	public class GetFloorPlanDoorMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			Room room = session.Room;
			if (room != null) {
				router.GetComposer<SetFloorPlanDoorMessageComposer> ().Compose (session, 
					room.Data.Model.Door,
					room.Data.Model.DoorOrientation);
			}
		}
	}
}

