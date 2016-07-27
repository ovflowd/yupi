using System;


namespace Yupi.Messages.User
{
	public class GetCatalogClubGiftsMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			router.GetComposer<LoadCatalogClubGiftsMessageComposer> ().Compose (session);
		}
	}
}

