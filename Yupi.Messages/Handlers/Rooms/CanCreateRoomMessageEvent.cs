using System;

namespace Yupi.Messages.Rooms
{
	public class CanCreateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<GameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CanCreateRoomMessageComposer> ().Compose (session);
		}
	}
}

