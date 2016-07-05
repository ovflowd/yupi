using System;

namespace Yupi.Messages.Rooms
{
	public class CanCreateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CanCreateRoomMessageComposer> ().Compose (session);
		}
	}
}

