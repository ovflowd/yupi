using System;


namespace Yupi.Messages.Catalog
{
	public class GetCatalogPageMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Net.ISession<IGameClient> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			uint pageId = message.GetUInt32();

			message.GetInteger(); // TODO unused

			CatalogPage cPage = Yupi.Emulator.Yupi.GetGame().GetCatalogManager().GetPage(pageId);

			if (cPage == null || !cPage.Enabled || !cPage.Visible || cPage.MinRank > session.GetHabbo().Rank)
				return;

			router.GetComposer<CataloguePageMessageComposer> ().Compose (session, cPage);
		}
	}
}

