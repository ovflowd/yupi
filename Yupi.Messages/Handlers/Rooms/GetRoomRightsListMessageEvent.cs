using System;

namespace Yupi.Messages.Rooms
{
	public class GetRoomRightsListMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadRoomRightsListMessageComposer> ().Compose (session, session.GetHabbo ().CurrentRoom);
		}
	}
}

