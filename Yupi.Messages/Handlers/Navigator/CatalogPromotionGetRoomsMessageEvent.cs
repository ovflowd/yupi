using System;

namespace Yupi.Messages.Navigator
{
	// TODO Isn't this navigator and not catalog?
	public class CatalogPromotionGetRoomsMessageEvent : AbstractHandler
	{
		public override void HandleMessage ( Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<CatalogPromotionGetRoomsMessageComposer> ().Compose (session, session.Info.UsersRooms);
		}
	}
}

