/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Items.Interactions;
using Yupi.Emulator.Game.Items.Interactions.Enums;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Pets;
using Yupi.Emulator.Game.Pets.Enums;
using Yupi.Emulator.Game.RoomBots;
using Yupi.Emulator.Game.SoundMachine;
using Yupi.Emulator.Game.SoundMachine.Songs;



namespace Yupi.Emulator.Game.Catalogs
{
    /// <summary>
    ///     Class Catalog.
    /// </summary>
     public class CatalogManager
    {
        /// <summary>
        ///     The last sent offer
        /// </summary>
     public static uint LastSentOffer;

        /// <summary>
        ///     The categories
        /// </summary>
     public HybridDictionary Categories;

        /// <summary>
        ///     The ecotron levels
        /// </summary>
     public List<int> EcotronLevels;

        /// <summary>
        ///     The ecotron rewards
        /// </summary>
     public List<EcotronReward> EcotronRewards;

        /// <summary>
        ///     The flat offers
        /// </summary>
     public Dictionary<uint, uint> FlatOffers;

        /// <summary>
        ///     The habbo club items
        /// </summary>
     public List<CatalogItem> HabboClubItems;

        /// <summary>
        ///     The offers
        /// </summary>
     public HybridDictionary Offers;

        /// <summary>
        ///     Checks the name of the pet.
        /// </summary>
        /// <param name="petName">Name of the pet.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
     public static bool CheckPetName(string petName)
            => petName.Length >= 3 && petName.Length <= 15 && Yupi.IsValidAlphaNumeric(petName);

        /// <summary>
        ///     Creates the pet.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="race">The race.</param>
        /// <param name="color">The color.</param>
        /// <param name="rarity">The rarity.</param>
        /// <returns>Pet.</returns>
     public static Pet CreatePet(uint userId, string name, string type, string race, string color, int rarity = 0)
        {
            uint trace = Convert.ToUInt32(race);

            uint petId = 404u;

            Pet pet = new Pet(petId, userId, 0u, name, type, trace, 0, 100, 150, 0, Yupi.GetUnixTimeStamp(), 0, 0, 0.0,
                false, 0, 0, -1, rarity, DateTime.Now.AddHours(36.0), DateTime.Now.AddHours(48.0), null, color)
            {
                DbState = DatabaseUpdateState.NeedsUpdate
            };

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "INSERT INTO pets_data (user_id, name, ai_type, pet_type, race_id, experience, energy, createstamp, rarity, lasthealth_stamp, untilgrown_stamp, color) " +
                    $"VALUES ('{pet.OwnerId}', @petName, 'pet', @petType, @petRace, 0, 100, UNIX_TIMESTAMP(), '{rarity}', UNIX_TIMESTAMP(now() + INTERVAL 36 HOUR), UNIX_TIMESTAMP(now() + INTERVAL 48 HOUR), @petColor)");

                queryReactor.AddParameter("petName", pet.Name);
                queryReactor.AddParameter("petType", pet.Type);
                queryReactor.AddParameter("petRace", pet.Race);
                queryReactor.AddParameter("petColor", pet.Color);

                petId = (uint) queryReactor.InsertQuery();
            }

            pet.PetId = petId;

            if (pet.Type == "pet_monster")
            {
                pet.MoplaBreed = MoplaBreed.CreateMonsterplantBreed(pet);
                pet.Name = pet.MoplaBreed.Name;
                pet.DbState = DatabaseUpdateState.NeedsUpdate;
            }

            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(userId);

            if (clientByUserId != null)
                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(clientByUserId, "ACH_PetLover", 1);

            return pet;
        }

        /// <summary>
        ///     Generates the pet from row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>Pet.</returns>
     public static Pet GeneratePetFromRow(DataRow row)
        {
            MoplaBreed moplaBreed = null;

            if ((string) row["pet_type"] == "pet_monster")
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery(
                        $"SELECT * FROM pets_plants WHERE pet_id = {Convert.ToUInt32(row["id"])}");

                    DataRow sRow = queryReactor.GetRow();

                    moplaBreed = new MoplaBreed(sRow);
                }
            }

            return new Pet(Convert.ToUInt32(row["id"]), Convert.ToUInt32(row["user_id"]),
                Convert.ToUInt32(row["room_id"]), (string) row["name"], (string) row["pet_type"],
                (uint) row["race_id"], (uint) row["experience"], (uint) row["energy"],
                (uint) row["nutrition"], (uint) row["respect"], Convert.ToDouble(row["createstamp"].ToString()),
                (int) row["x"],
                (int) row["y"], Convert.ToDouble(row["z"]), Convert.ToInt32(row["have_saddle"].ToString()) == 1,
                Convert.ToInt32(row["anyone_ride"].ToString()),
                (int) row["hairdye"], (int) row["pethair"], (int) row["rarity"],
                Yupi.UnixToDateTime((int) row["lasthealth_stamp"]),
                Yupi.UnixToDateTime((int) row["untilgrown_stamp"]), moplaBreed, (string) row["color"]);
        }

        /// <summary>
        ///     Gets the item from offer.
        /// </summary>
        /// <param name="offerId">The offer identifier.</param>
        /// <returns>CatalogItem.</returns>
     public CatalogItem GetItemFromOffer(uint offerId)
        {
            CatalogItem result = null;

            if (FlatOffers.ContainsKey(offerId))
                result = (CatalogItem) Offers[FlatOffers[offerId]];

            return result ?? Yupi.GetGame().GetCatalogManager().GetItem(offerId);
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="pageLoaded">The page loaded.</param>
     public void Init(out int pageLoaded)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                Init(queryReactor);

            pageLoaded = Categories.Count;
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
     public void Init(IQueryAdapter dbClient)
        {
            Categories = new HybridDictionary();
            Offers = new HybridDictionary();
            FlatOffers = new Dictionary<uint, uint>();
            EcotronRewards = new List<EcotronReward>();
            EcotronLevels = new List<int>();
            HabboClubItems = new List<CatalogItem>();

            dbClient.SetQuery("SELECT * FROM catalog_items ORDER BY id ASC");
            DataTable itemsTable = dbClient.GetTable();
            dbClient.SetQuery("SELECT * FROM catalog_pages ORDER BY id ASC");
            DataTable pagesTable = dbClient.GetTable();
            dbClient.SetQuery("SELECT * FROM catalog_ecotron_items ORDER BY reward_level ASC");
            DataTable ecotronItems = dbClient.GetTable();
            dbClient.SetQuery("SELECT * FROM catalog_items WHERE special_name LIKE 'HABBO_CLUB_VIP%'");
            DataTable habboClubItems = dbClient.GetTable();

            if (itemsTable != null)
            {
                foreach (DataRow dataRow in itemsTable.Rows)
                {
                    if (string.IsNullOrEmpty(dataRow["item_names"].ToString()) || string.IsNullOrEmpty(dataRow["amounts"].ToString()))
                        continue;

                    string source = dataRow["item_names"].ToString();
                    string firstItem = dataRow["item_names"].ToString().Split(';')[0];

                    Item item;

                    if (!Yupi.GetGame().GetItemManager().GetItem(firstItem, out item))
                        continue;

                    uint num = !source.Contains(';') ? item.FlatId : 0;

                    if (!dataRow.IsNull("special_name"))
                        item.PublicName = (string) dataRow["special_name"];

                    CatalogItem catalogItem = new CatalogItem(dataRow, item.PublicName);

                    if (catalogItem.GetFirstBaseItem() == null)
                        continue;

                    Offers.Add(catalogItem.Id, catalogItem);

                    if (!FlatOffers.ContainsKey(num))
                        FlatOffers.Add(num, catalogItem.Id);
                }
            }

            if (pagesTable != null)
            {
                foreach (DataRow dataRow2 in pagesTable.Rows)
                {
                    bool visible = false;
                    bool enabled = false;

                    if (dataRow2["visible"].ToString() == "1")
                        visible = true;

                    if (dataRow2["enabled"].ToString() == "1")
                        enabled = true;

                    Categories.Add((uint) dataRow2["id"],
                        new CatalogPage((uint) dataRow2["id"], short.Parse(dataRow2["parent_id"].ToString()),
                            (string) dataRow2["code_name"], (string) dataRow2["caption"], visible, enabled, false,
                            (uint) dataRow2["min_rank"], (int) dataRow2["icon_image"],
                            (string) dataRow2["page_layout"], (string) dataRow2["page_headline"],
                            (string) dataRow2["page_teaser"], (string) dataRow2["page_special"],
                            (string) dataRow2["page_text1"], (string) dataRow2["page_text2"],
                            (string) dataRow2["page_text_details"], (string) dataRow2["page_text_teaser"],
                            (string) dataRow2["page_link_description"], (string) dataRow2["page_link_pagename"],
                            (int) dataRow2["order_num"], ref Offers));
                }
            }

            if (ecotronItems != null)
            {
                foreach (DataRow dataRow3 in ecotronItems.Rows)
                {
                    EcotronRewards.Add(new EcotronReward(Convert.ToUInt32(dataRow3["display_id"]),
                        Convert.ToUInt32(dataRow3["item_id"]), Convert.ToUInt32(dataRow3["reward_level"])));

                    if (!EcotronLevels.Contains(Convert.ToInt16(dataRow3["reward_level"])))
                        EcotronLevels.Add(Convert.ToInt16(dataRow3["reward_level"]));
                }
            }

            if (habboClubItems != null)
            {
                foreach (DataRow row in habboClubItems.Rows)
                    HabboClubItems.Add(new CatalogItem(row,
                        row.IsNull("special_name") ? "Habbo VIP" : (string) row["special_name"]));
            }
        }

        /// <summary>
        ///     Gets the item.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>CatalogItem.</returns>
     public CatalogItem GetItem(uint itemId) => Offers.Contains(itemId) ? (CatalogItem) Offers[itemId] : null;

        /// <summary>
        ///     Gets the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>CatalogPage.</returns>
     public CatalogPage GetPage(uint page) => Categories.Contains(page) ? (CatalogPage) Categories[page] : null;

     public void HandlePurchase(GameClient session, uint pageId, uint itemId, string extraData, uint priceAmount,
            bool isGift, string giftUser, string giftMessage, int giftSpriteId, int giftLazo, int giftColor, bool undef,
            uint theGroup)
        {
            priceAmount = priceAmount < 1 || priceAmount > 100 ? 1 : priceAmount;

            uint totalPrice = priceAmount, limitedId = 0, limtot = 0;

            if (priceAmount >= 6)
                totalPrice -= Convert.ToUInt32(Math.Ceiling(Convert.ToDouble(priceAmount)/6))*2 - 1;

            if (!Categories.Contains(pageId))
                return;

            CatalogPage catalogPage = GetPage(pageId);

            if (catalogPage == null || !catalogPage.Enabled || !catalogPage.Visible || session?.GetHabbo() == null ||
                catalogPage.MinRank > session.GetHabbo().Rank || catalogPage.Layout == "sold_ltd_items")
                return;

            CatalogItem item = catalogPage.GetItem(itemId);

            if (item == null || session.GetHabbo().Credits < item.CreditsCost ||
                session.GetHabbo().Duckets < item.DucketsCost || session.GetHabbo().Diamonds < item.DiamondsCost ||
                item.Name == "room_ad_plus_badge")
                return;

            #region Habbo Club Purchase

            if (catalogPage.Layout == "vip_buy" || catalogPage.Layout == "club_buy" || HabboClubItems.Contains(item))
            {
                string[] array = item.Name.Split('_');

                double dayLength = 31;

                if (item.Name.Contains("DAY"))
                    dayLength = double.Parse(array[3]);
                else if (item.Name.Contains("MONTH"))
                    dayLength = Math.Ceiling(double.Parse(array[3])*31 - 0.205);
                else if (item.Name.Contains("YEAR"))
                    dayLength = double.Parse(array[3])*31*12;

                session.GetHabbo().GetSubscriptionManager().AddSubscription(dayLength);

                if (item.CreditsCost > 0)
                {
                    session.GetHabbo().Credits -= item.CreditsCost*totalPrice;
                    session.GetHabbo().UpdateCreditsBalance();
                }

                if (item.DucketsCost > 0)
                {
                    session.GetHabbo().Duckets -= item.DucketsCost*totalPrice;
                    session.GetHabbo().UpdateActivityPointsBalance();
                }

                if (item.DiamondsCost > 0)
                {
                    session.GetHabbo().Diamonds -= item.DiamondsCost*totalPrice;
                    session.GetHabbo().UpdateSeasonalCurrencyBalance();
                }

                return;
            }

            #endregion

            uint toUserId = 0u;

            foreach (Item baseItem in item.Items.Keys)
            {
                #region Item is Builders Club Addon

                if (item.Name.StartsWith("builders_club_addon_"))
                {
                    int furniAmount =
                        int.Parse(item.Name.Replace("builders_club_addon_", string.Empty)
                            .Replace("furnis", string.Empty));

                    session.GetHabbo().BuildersItemsMax += furniAmount;

					session.Router.GetComposer<BuildersClubMembershipMessageComposer>().Compose(session, session.GetHabbo().BuildersExpire,
						session.GetHabbo().BuildersItemsMax);      

                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery("UPDATE users SET builders_items_max = @max WHERE id = @userId");
                        queryReactor.AddParameter("max", session.GetHabbo().BuildersItemsMax);
                        queryReactor.AddParameter("userId", session.GetHabbo().Id);
                        queryReactor.RunQuery();
                    }

					session.Router.GetComposer<PurchaseOKMessageComposer> ().Compose (session, item, item.Items);
                    session.SendNotif("${notification.builders_club.membership_extended.message}",
                        "${notification.builders_club.membership_extended.title}", "builders_club_membership_extended");

                    return;
                }

                #endregion

                #region Item is Builders Club Upgrade

                if (item.Name.StartsWith("builders_club_time_"))
                {
                    int timeAmount =
                        int.Parse(item.Name.Replace("builders_club_time_", string.Empty)
                            .Replace("seconds", string.Empty));

                    session.GetHabbo().BuildersExpire += timeAmount;

					session.Router.GetComposer<BuildersClubMembershipMessageComposer>().Compose(session, session.GetHabbo().BuildersExpire,
						session.GetHabbo().BuildersItemsMax);  
					
                    using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor.SetQuery("UPDATE users SET builders_expire = @max WHERE id = @userId");
                        queryReactor.AddParameter("max", session.GetHabbo().BuildersExpire);
                        queryReactor.AddParameter("userId", session.GetHabbo().Id);
                        queryReactor.RunQuery();
                    }

					session.Router.GetComposer<PurchaseOKMessageComposer> ().Compose (session, item, item.Items);
                    session.SendNotif("${notification.builders_club.membership_extended.message}",
                        "${notification.builders_club.membership_extended.title}", "builders_club_membership_extended");

                    return;
                }

                #endregion
			}
        }

        /// <summary>
        ///     Gets the random ecotron reward.
        /// </summary>
        /// <returns>EcotronReward.</returns>
     public EcotronReward GetRandomEcotronReward()
        {
            uint level = 1u;

            if (Yupi.GetRandomNumber(1, 2000) == 2000)
                level = 5u;
            else if (Yupi.GetRandomNumber(1, 200) == 200)
                level = 4u;
            else if (Yupi.GetRandomNumber(1, 40) == 40)
                level = 3u;
            else if (Yupi.GetRandomNumber(1, 4) == 4)
                level = 2u;

            List<EcotronReward> ecotronRewardsForLevel = GetEcotronRewardsForLevel(level);

            return ecotronRewardsForLevel[Yupi.GetRandomNumber(0, ecotronRewardsForLevel.Count - 1)];
        }

        /// <summary>
        ///     Gets the ecotron rewards.
        /// </summary>
        /// <returns>List&lt;EcotronReward&gt;.</returns>
     public List<EcotronReward> GetEcotronRewards()
        {
            return EcotronRewards;
        }

        /// <summary>
        ///     Gets the ecotron rewards levels.
        /// </summary>
        /// <returns>List&lt;System.Int32&gt;.</returns>
     public List<int> GetEcotronRewardsLevels()
        {
            return EcotronLevels;
        }

        /// <summary>
        ///     Gets the ecotron rewards for level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>List&lt;EcotronReward&gt;.</returns>
     public List<EcotronReward> GetEcotronRewardsForLevel(uint level)
        {
            return EcotronRewards.Where(current => current.RewardLevel == level).ToList();
        }
    }
}