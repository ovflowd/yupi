using System;


namespace Yupi.Messages.Rooms
{
	public class GetFloorPlanDoorMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Room room = session.GetHabbo().CurrentRoom;

			if (room != null) {
				router.GetComposer<SetFloorPlanDoorMessageComposer> ().Compose (session, 
					room.GetGameMap ().Model.DoorX,
					room.GetGameMap ().Model.DoorY,
					room.GetGameMap ().Model.DoorOrientation);
			}
			*/
			throw new NotImplementedException ();
		}
	}
}

