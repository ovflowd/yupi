using System;

namespace Yupi.Messages.Catalog
{
	public class GetCatalogClubPageMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int windowId = message.GetInteger();
			router.GetComposer<CatalogueClubPageMessageComposer> ().Compose (session, windowId);
		}
	}
}

