using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Catalog
{
	public class CataloguePageMessageComposer : AbstractComposer<CatalogPage>
	{
		public override void Compose ( Yupi.Protocol.ISender session, CatalogPage page)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(page.Id);

				// TODO Refactor

				switch (page.Layout)
				{
				case "frontpage":
					message.AppendString("NORMAL");
					message.AppendString("frontpage4");
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(2);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendInteger(0);
					message.AppendInteger(-1);
					message.AppendBool(false);
					break;

				case "roomads":
					message.AppendString("NORMAL");
					message.AppendString("roomads");
					message.AppendInteger(2);
					message.AppendString("events_header");
					message.AppendString("");
					message.AppendInteger(2);
					message.AppendString(page.Text1);
					message.AppendString("");
					break;

				case "builders_club_frontpage_normal":
					message.AppendString("NORMAL");
					message.AppendString("builders_club_frontpage");
					message.AppendInteger(0);
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					message.AppendInteger(3);
					message.AppendInteger(8554);
					message.AppendString("builders_club_1_month");
					message.AppendString("");
					message.AppendInteger(2560000);
					message.AppendInteger(2560000);
					message.AppendInteger(1024);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					message.AppendInteger(8606);
					message.AppendString("builders_club_14_days");
					message.AppendString("");
					message.AppendInteger(2560000);
					message.AppendInteger(2560000);
					message.AppendInteger(1024);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					message.AppendInteger(8710);
					message.AppendString("builders_club_31_days");
					message.AppendString("");
					message.AppendInteger(2560000);
					message.AppendInteger(2560000);
					message.AppendInteger(1024);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					break;

				case "bots":
					message.AppendString("NORMAL");
					message.AppendString("bots");
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendString(page.TextDetails);
					break;

				case "badge_display":
					message.AppendString("NORMAL");
					message.AppendString("badge_display");
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendString(page.TextDetails);
					break;

				case "info_loyalty":
				case "info_duckets":
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					message.AppendInteger(1);
					message.AppendString(page.Text1);
					break;

				case "sold_ltd_items":
					message.AppendString("NORMAL");
					message.AppendString("sold_ltd_items");
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendString(page.TextDetails);
					break;

				case "recycler_info":
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendString(page.TextDetails);
					break;

				case "recycler_prizes":
					message.AppendString("NORMAL");
					message.AppendString("recycler_prizes");
					message.AppendInteger(1);
					message.AppendString("catalog_recycler_headline3");
					message.AppendInteger(1);
					message.AppendString(page.Text1);
					break;

				case "spaces_new":
				case "spaces":
					message.AppendString("NORMAL");
					message.AppendString("spaces_new");
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					message.AppendInteger(1);
					message.AppendString(page.Text1);
					break;

				case "recycler":
					message.AppendString("NORMAL");
					message.AppendString("recycler");
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(1);
					message.AppendString(page.Text1);
					break;

				case "trophies":
					message.AppendString("NORMAL");
					message.AppendString("trophies");
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					message.AppendInteger(2);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails);
					break;

				case "pets":
				case "pets2":
				case "pets3":
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(4);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendString(page.TextDetails);
					message.AppendString(page.TextTeaser);
					break;

				case "soundmachine":
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(2);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails);
					break;

				case "vip_buy":
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(0);
					break;

				case "guild_custom_furni":
					message.AppendString("NORMAL");
					message.AppendString("guild_custom_furni");
					message.AppendInteger(3);
					message.AppendString(page.LayoutHeadline);
					message.AppendString("");
					message.AppendString("");
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails);
					message.AppendString(page.Text2);
					break;

				case "guild_frontpage":
					message.AppendString("NORMAL");
					message.AppendString("guild_frontpage");
					message.AppendInteger(2);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails);
					message.AppendString(page.Text2);
					break;

				case "guild_forum":
					message.AppendString("NORMAL");
					message.AppendString("guild_forum");
					message.AppendInteger(0);
					message.AppendInteger(2);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					break;

				case "club_gifts":
					message.AppendString("NORMAL");
					message.AppendString("club_gifts");
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					message.AppendInteger(1);
					message.AppendString(page.Text1);
					break;

				case "default_3x3":
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(3);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendString(page.LayoutSpecial);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails);
					message.AppendString(page.TextTeaser);
					break;

				default:
					message.AppendString("NORMAL");
					message.AppendString(page.Layout);
					message.AppendInteger(3);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendString(page.LayoutSpecial);
					message.AppendInteger(4);
					message.AppendString(page.Text1);
					message.AppendString(page.Text2);
					message.AppendString(page.TextTeaser);
					message.AppendString(page.TextDetails);
					break;

				case "builders_3x3":
					message.AppendString("BUILDERS_CLUB");
					message.AppendString("default_3x3");
					message.AppendInteger(3);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendString(page.LayoutSpecial);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails.Replace("[10]", Convert.ToChar(10).ToString())
						.Replace("[13]", Convert.ToChar(13).ToString()));
					message.AppendString(page.TextTeaser.Replace("[10]", Convert.ToChar(10).ToString())
						.Replace("[13]", Convert.ToChar(13).ToString()));
					break;

				case "builders_3x3_color":
					message.AppendString("BUILDERS_CLUB");
					message.AppendString("default_3x3_color_grouping");
					message.AppendInteger(3);
					message.AppendString(page.LayoutHeadline);
					message.AppendString(page.LayoutTeaser);
					message.AppendString(page.LayoutSpecial);
					message.AppendInteger(3);
					message.AppendString(page.Text1);
					message.AppendString(page.TextDetails.Replace("[10]", Convert.ToChar(10).ToString())
						.Replace("[13]", Convert.ToChar(13).ToString()));
					message.AppendString(page.TextTeaser.Replace("[10]", Convert.ToChar(10).ToString())
						.Replace("[13]", Convert.ToChar(13).ToString()));
					break;

				case "builders_club_frontpage":
					message.AppendString("BUILDERS_CLUB");
					message.AppendString("builders_club_frontpage");
					message.AppendInteger(0);
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					message.AppendInteger(3);
					message.AppendInteger(8554);
					message.AppendString("builders_club_1_month");
					message.AppendString("");
					message.AppendInteger(2560000);
					message.AppendInteger(2560000);
					message.AppendInteger(1024);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					message.AppendInteger(8606);
					message.AppendString("builders_club_14_days");
					message.AppendString("");
					message.AppendInteger(2560000);
					message.AppendInteger(2560000);
					message.AppendInteger(1024);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					message.AppendInteger(8710);
					message.AppendString("builders_club_31_days");
					message.AppendString("");
					message.AppendInteger(2560000);
					message.AppendInteger(2560000);
					message.AppendInteger(1024);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					break;

				case "builders_club_addons":
					message.AppendString("BUILDERS_CLUB");
					message.AppendString("builders_club_addons");
					message.AppendInteger(0);
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					break;

				case "builders_club_addons_normal":
					message.AppendString("NORMAL");
					message.AppendString("builders_club_addons");
					message.AppendInteger(0);
					message.AppendInteger(1);
					message.AppendString(page.LayoutHeadline);
					break;
				}

				if (page.Layout.StartsWith("frontpage") || page.Layout == "vip_buy" || page.Layout == "recycler")
				{
					message.AppendInteger(0);
				}
				else
				{
					message.AppendInteger(page.Items.Count);

					foreach (CatalogItem item in page.Items.Values)
						ComposeItem(item, message);
				}

				message.AppendInteger(-1);
				message.AppendBool(false);

				session.Send (message);
			}
		}
			
		private void ComposeItem(CatalogItem item, ServerMessage messageBuffer)
		{
			messageBuffer.AppendInteger(item.Id);

			string displayName = item.Name;

			if (PetTypeManager.ItemIsPet(item.Name))
				displayName = PetTypeManager.GetHabboPetType(item.Name);

			messageBuffer.AppendString(displayName);
			messageBuffer.AppendBool(false);
			messageBuffer.AppendInteger(item.CreditsCost);

			if (item.DiamondsCost > 0)
			{
				messageBuffer.AppendInteger(item.DiamondsCost);
				messageBuffer.AppendInteger(105);
			}
			else
			{
				messageBuffer.AppendInteger(item.DucketsCost);
				messageBuffer.AppendInteger(0);
			}
			messageBuffer.AppendBool(item.GetFirstBaseItem().AllowGift);

			switch (item.Name)
			{
			case "g0 group_product":
				messageBuffer.AppendInteger(0);
				break;

			case "room_ad_plus_badge":
				messageBuffer.AppendInteger(1);
				messageBuffer.AppendString("b");
				messageBuffer.AppendString("RADZZ");
				break;

			default:
				if (item.Name.StartsWith("builders_club_addon_") || item.Name.StartsWith("builders_club_time_"))
					messageBuffer.AppendInteger(0);
				else if (item.Badge == "")
					messageBuffer.AppendInteger(item.Items.Count);
				else
				{
					messageBuffer.AppendInteger(item.Items.Count + 1);
					messageBuffer.AppendString("b");
					messageBuffer.AppendString(item.Badge);
				}
				break;
			}
			foreach (Item baseItem in item.Items.Keys)
			{
				if (item.Name == "g0 group_product" || item.Name.StartsWith("builders_club_addon_") ||
					item.Name.StartsWith("builders_club_time_"))
					break;
				if (item.Name == "room_ad_plus_badge")
				{
					messageBuffer.AppendString("");
					messageBuffer.AppendInteger(0);
				}
				else
				{
					messageBuffer.AppendString(baseItem.Type.ToString());
					messageBuffer.AppendInteger(baseItem.SpriteId);

					if (item.Name.Contains("wallpaper_single") || item.Name.Contains("floor_single") ||
						item.Name.Contains("landscape_single"))
					{
						string[] array = item.Name.Split('_');
						messageBuffer.AppendString(array[2]);
					}
					else if (item.Name.StartsWith("bot_") || baseItem.InteractionType == Interaction.MusicDisc ||
						item.GetFirstBaseItem().Name == "poster")
						messageBuffer.AppendString(item.ExtraData);
					else if (item.Name.StartsWith("poster_"))
					{
						string[] array2 = item.Name.Split('_');
						messageBuffer.AppendString(array2[1]);
					}
					else if (item.Name.StartsWith("poster "))
					{
						string[] array3 = item.Name.Split(' ');
						messageBuffer.AppendString(array3[1]);
					}
					else if (item.SongId > 0u && baseItem.InteractionType == Interaction.MusicDisc)
						messageBuffer.AppendString(item.ExtraData);
					else
						messageBuffer.AppendString(string.Empty);

					messageBuffer.AppendInteger(item.Items[baseItem]);
					messageBuffer.AppendBool(item.IsLimited);
					if (!item.IsLimited)
						continue;
					messageBuffer.AppendInteger(item.LimitedStack);
					messageBuffer.AppendInteger(item.LimitedStack - item.LimitedSelled);
				}
			}
			messageBuffer.AppendInteger(item.ClubOnly ? 1 : 0);

			if (item.IsLimited || item.FirstAmount != 1)
			{
				messageBuffer.AppendBool(false);
				return;
			}

			messageBuffer.AppendBool(item.HaveOffer && !item.IsLimited);
		}
	}
}

