using System;
using Yupi.Protocol.Buffers;

using System.Collections.Generic;
using Yupi.Model.Domain;



namespace Yupi.Messages.Catalog
{
	public class PurchaseOKMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, CatalogItem itemCatalog, Dictionary<BaseItem, uint> items,
			int clubLevel = 1)
		{
			Compose(session, itemCatalog.Id, itemCatalog.Name, itemCatalog.CreditsCost, items, clubLevel,
				itemCatalog.DiamondsCost,
				itemCatalog.DucketsCost, itemCatalog.IsLimited, itemCatalog.LimitedStack, itemCatalog.LimitedSelled);
		}

		// TODO There should be no need to expose such a complex method signature
		public void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, uint itemId, string itemName, uint creditsCost,
		                    Dictionary<BaseItem, uint> items = null, int clubLevel = 1,
		                    uint diamondsCost = 0,
		                    uint activityPointsCost = 0, bool isLimited = false,
		                    uint limitedStack = 0,
		                    uint limitedSelled = 0)
		{

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (itemId);
				message.AppendString (itemName);
				message.AppendBool (false); // TODO Hardcoded
				message.AppendInteger (creditsCost);
				message.AppendInteger (diamondsCost);
				message.AppendInteger (activityPointsCost);
				message.AppendBool (true);
				message.AppendInteger (items?.Count ?? 0);

				if (items != null) {
					foreach (KeyValuePair<BaseItem, uint> itemDic in items) {
						BaseItem item = itemDic.Key;
						message.AppendString (item.Type.ToString ());

						if (item.Type == 'b') {
							message.AppendString (item.PublicName);
							continue;
						}

						message.AppendInteger (item.SpriteId);
						message.AppendString (item.PublicName);
						message.AppendInteger (itemDic.Value); //productCount
						message.AppendBool (isLimited);

						if (!isLimited)
							continue;

						message.AppendInteger (limitedStack);
						message.AppendInteger (limitedSelled);
					}
				}

				message.AppendInteger (clubLevel);
				message.AppendBool (false); //window.visible?

				session.Send (message);
			}
		}
	}
}

