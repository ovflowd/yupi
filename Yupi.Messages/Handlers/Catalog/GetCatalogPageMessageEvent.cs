using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;


namespace Yupi.Messages.Catalog
{
	public class GetCatalogPageMessageEvent : AbstractHandler
	{
		private Repository<CatalogPage> CatalogRepository;

		public GetCatalogPageMessageEvent ()
		{
			CatalogRepository = DependencyFactory.Resolve<Repository<CatalogPage>> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			int pageId = message.GetInteger();

			message.GetInteger(); // TODO unused

			CatalogPage page = CatalogRepository.FindBy (pageId);

			if (page == null || !page.Enabled || !page.Visible || page.MinRank > session.UserData.Info.Rank)
				return;

			router.GetComposer<CataloguePageMessageComposer> ().Compose (session, page);
		}
	}
}

