using System;


namespace Yupi.Messages.Rooms
{
	public class RoomGetSettingsInfoMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			/*
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(request.GetUInt32());
			if (room == null)
				return;

			router.GetComposer<RoomSettingsDataMessageComposer> ().Compose (session, room);
			*/
			throw new NotImplementedException ();
		}
	}
}

