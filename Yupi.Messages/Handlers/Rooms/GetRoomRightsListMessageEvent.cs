using System;

namespace Yupi.Messages.Rooms
{
	public class GetRoomRightsListMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadRoomRightsListMessageComposer> ().Compose (session, session.GetHabbo ().CurrentRoom);
		}
	}
}

