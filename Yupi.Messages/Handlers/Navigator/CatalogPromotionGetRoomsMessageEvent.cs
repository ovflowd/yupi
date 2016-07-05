using System;

namespace Yupi.Messages.Navigator
{
	// TODO Isn't this navigator and not catalog?
	public class CatalogPromotionGetRoomsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CatalogPromotionGetRoomsMessageComposer> ().Compose (session, session.GetHabbo ().UsersRooms);
		}
	}
}

