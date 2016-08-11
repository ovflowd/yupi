using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Catalog
{
	public class GetCatalogIndexMessageEvent : AbstractHandler
	{
		private Repository<CatalogPage> CatalogRepository;

		public GetCatalogIndexMessageEvent ()
		{
			CatalogRepository = DependencyFactory.Resolve<Repository<CatalogPage>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			List<CatalogPage> pages = CatalogRepository
				.FilterBy (x => x.Parent == null && x.MinRank <= session.UserData.Info.Rank)
				.OrderBy (x => x.OrderNum).ToList();

			// TODO Type?!
			string type = message.GetString ().ToUpper ();

			router.GetComposer<CatalogueOfferConfigMessageComposer> ().Compose (session);
			router.GetComposer<CatalogueIndexMessageComposer> ().Compose (session, pages, type, session.UserData.Info.Rank);
		}
	}
}

