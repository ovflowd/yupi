using System;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Messages.Catalog
{
	public class GetCatalogIndexMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage message, Router router)
		{
			// TODO Do categories contain other things than pages and if so why?
			IEnumerable<CatalogPage> pages =
				Yupi.Emulator.Yupi.GetGame().GetCatalogManager().Categories.Values.OfType<CatalogPage>().ToList();

			// TODO Should already be sorted when loading !!!
			IOrderedEnumerable<CatalogPage> sortedPages = pages.Where(x => x.ParentId == -2 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

			string type = message.GetString ().ToUpper ();

			if (type == "NORMAL")
				sortedPages = pages.Where(x => x.ParentId == -1 && x.MinRank <= rank).OrderBy(x => x.OrderNum);

			router.GetComposer<CatalogueOfferConfigMessageComposer> ().Compose (session);
			router.GetComposer<CatalogueIndexMessageComposer> ().Compose (session, sortedPages, pages, type);
		}
	}
}

