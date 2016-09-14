using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;

namespace Yupi.Messages.Catalog
{
	public class CatalogueClubPageMessageComposer : Yupi.Messages.Contracts.CatalogueClubPageMessageComposer
	{
		private IRepository<CatalogPage> CatalogRepository;

		public CatalogueClubPageMessageComposer ()
		{
			CatalogRepository = DependencyFactory.Resolve<IRepository<CatalogPage>> ();
		}

		public override void Compose ( Yupi.Protocol.ISender session, int windowId)
		{
			// TODO Refactor
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				throw new NotImplementedException ();
				/*
				List<CatalogItem> habboClubItems = Yupi.GetGame ().GetCatalogManager ().HabboClubItems;

				message.AppendInteger (habboClubItems.Count);

				foreach (CatalogItem item in habboClubItems) {
					message.AppendInteger (item.Id);
					message.AppendString (item.Name);
					message.AppendBool (false);
					message.AppendInteger (item.CreditsCost);

					if (item.DiamondsCost > 0) {
						message.AppendInteger (item.DiamondsCost);
						message.AppendInteger (105);
					} else {
						message.AppendInteger (item.DucketsCost);
						message.AppendInteger (0);
					}

					message.AppendBool (true);
					string[] fuckingArray = item.Name.Split ('_');
					double dayTime = 31;

					if (item.Name.Contains ("DAY"))
						dayTime = int.Parse (fuckingArray [3]);
					else if (item.Name.Contains ("MONTH")) {
						int monthTime = int.Parse (fuckingArray [3]);
						dayTime = monthTime * 31;
					} else if (item.Name.Contains ("YEAR")) {
						int yearTimeOmg = int.Parse (fuckingArray [3]);
						dayTime = yearTimeOmg * 31 * 12;
					}

					DateTime newExpiryDate = DateTime.Now.AddDays (dayTime);

					if (session.GetHabbo ().GetSubscriptionManager ().HasSubscription)
						newExpiryDate =
						Yupi.UnixToDateTime (session.GetHabbo ().GetSubscriptionManager ().GetSubscription ().ExpireTime)
							.AddDays (dayTime);

					message.AppendInteger ((int)dayTime / 31);
					message.AppendInteger ((int)dayTime);
					message.AppendBool (false);
					message.AppendInteger ((int)dayTime);
					message.AppendInteger (newExpiryDate.Year);
					message.AppendInteger (newExpiryDate.Month);
					message.AppendInteger (newExpiryDate.Day);
				}

				message.AppendInteger (windowId);
				session.Send(message);
				*/
			}
		}
	}
}

