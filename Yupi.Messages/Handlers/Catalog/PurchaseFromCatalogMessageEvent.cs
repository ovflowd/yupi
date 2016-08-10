using System;
using Yupi.Messages.Notification;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;

namespace Yupi.Messages.Catalog
{
	public class PurchaseFromCatalogMessageEvent : AbstractHandler
	{
		private CatalogController CatalogController;

		public PurchaseFromCatalogMessageEvent ()
		{
			CatalogController = DependencyFactory.Resolve<CatalogController> ();
		}

		public override void HandleMessage ( Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, Yupi.Protocol.Buffers.ClientMessage message, Yupi.Protocol.IRouter router)
		{
			// TODO Maximum items

			int pageId = message.GetInteger();
			int itemId = message.GetInteger();
			string extraData = message.GetString();
			int amount = message.GetInteger();

			CatalogItem item = CatalogController.GetById (pageId, itemId);
			CatalogController.Purchase (session.UserData, item, extraData, amount);
		}
	}
}

