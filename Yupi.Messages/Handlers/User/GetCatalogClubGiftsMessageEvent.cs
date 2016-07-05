using System;


namespace Yupi.Messages.User
{
	public class GetCatalogClubGiftsMessageEvent : AbstractHandler
	{
		public void HandleMessage (GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			router.GetComposer<LoadCatalogClubGiftsMessageComposer> ().Compose (session);
		}
	}
}

