using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Catalog
{
	public class CatalogOfferMessageComposer : AbstractComposer
	{
		public void Compose (Yupi.Protocol.ISender session, CatalogItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (item.Id);

				string displayName = item.Name;

				if (PetTypeManager.ItemIsPet (item.Name))
					displayName = PetTypeManager.GetHabboPetType (item.Name);

				message.AppendString (displayName);
				message.AppendBool (false);
				message.AppendInteger (item.CreditsCost);

				if (item.DiamondsCost > 0) {
					message.AppendInteger (item.DiamondsCost);
					message.AppendInteger (105);
				} else {
					message.AppendInteger (item.DucketsCost);
					message.AppendInteger (0);
				}
				message.AppendBool (item.GetFirstBaseItem ().AllowGift);
				// TODO Refactor
				switch (item.Name) {
				case "g0 group_product":
					message.AppendInteger (0);
					break;

				case "room_ad_plus_badge":
					message.AppendInteger (1);
					message.AppendString ("b");
					message.AppendString ("RADZZ");
					break;

				default:
					if (item.Name.StartsWith ("builders_club_addon_") || item.Name.StartsWith ("builders_club_time_"))
						message.AppendInteger (0);
					else if (item.Badge == "")
						message.AppendInteger (item.Items.Count);
					else {
						message.AppendInteger (item.Items.Count + 1);
						message.AppendString ("b");
						message.AppendString (item.Badge);
					}
					break;
				}
				foreach (Item baseItem in item.Items.Keys) {
					if (item.Name == "g0 group_product" || item.Name.StartsWith ("builders_club_addon_") ||
					   item.Name.StartsWith ("builders_club_time_"))
						break;
					if (item.Name == "room_ad_plus_badge") {
						message.AppendString ("");
						message.AppendInteger (0);
					} else {
						message.AppendString (baseItem.Type.ToString ());
						message.AppendInteger (baseItem.SpriteId);

						if (item.Name.Contains ("wallpaper_single") || item.Name.Contains ("floor_single") ||
						   item.Name.Contains ("landscape_single")) {
							string[] array = item.Name.Split ('_');
							message.AppendString (array [2]);
						} else if (item.Name.StartsWith ("bot_") || baseItem.InteractionType == Interaction.MusicDisc ||
						        item.GetFirstBaseItem ().Name == "poster")
							message.AppendString (item.ExtraData);
						else if (item.Name.StartsWith ("poster_")) {
							string[] array2 = item.Name.Split ('_');
							message.AppendString (array2 [1]);
						} else if (item.Name.StartsWith ("poster ")) {
							string[] array3 = item.Name.Split (' ');
							message.AppendString (array3 [1]);
						} else if (item.SongId > 0u && baseItem.InteractionType == Interaction.MusicDisc)
							message.AppendString (item.ExtraData);
						else
							message.AppendString (string.Empty);

						message.AppendInteger (item.Items [baseItem]);
						message.AppendBool (item.IsLimited);
						if (!item.IsLimited)
							continue;
						message.AppendInteger (item.LimitedStack);
						message.AppendInteger (item.LimitedStack - item.LimitedSelled);
					}
				}
				message.AppendInteger (item.ClubOnly ? 1 : 0);

				if (item.IsLimited || item.FirstAmount != 1) {
					message.AppendBool (false);
					return;
				}

				message.AppendBool (item.HaveOffer && !item.IsLimited);

				session.Send (message);
			}
		}
	}
}

