using System;

namespace Yupi.Messages.Navigator
{
	// TODO Isn't this navigator and not catalog?
	public class CatalogPromotionGetRoomsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CatalogPromotionGetRoomsMessageComposer> ().Compose (session, session.GetHabbo ().UsersRooms);
		}
	}
}

