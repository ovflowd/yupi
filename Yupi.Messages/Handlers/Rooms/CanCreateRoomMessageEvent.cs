using System;

namespace Yupi.Messages.Rooms
{
	public class CanCreateRoomMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CanCreateRoomMessageComposer> ().Compose (session);
		}
	}
}

