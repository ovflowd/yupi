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
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Catalogs.Composers;
using Yupi.Game.Catalogs.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Groups.Structs;
using Yupi.Game.Items.Interactions;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.Pets.Enums;
using Yupi.Game.RoomBots;
using Yupi.Game.SoundMachine;
using Yupi.Game.SoundMachine.Songs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Catalogs
{
    /// <summary>
    ///     Class Catalog.
    /// </summary>
    internal class CatalogManager
    {
        /// <summary>
        ///     The last sent offer
        /// </summary>
        internal static uint LastSentOffer;

        /// <summary>
        ///     The categories
        /// </summary>
        internal HybridDictionary Categories;

        /// <summary>
        ///     The ecotron levels
        /// </summary>
        internal List<int> EcotronLevels;

        /// <summary>
        ///     The ecotron rewards
        /// </summary>
        internal List<EcotronReward> EcotronRewards;

        /// <summary>
        ///     The flat offers
        /// </summary>
        internal Dictionary<uint, uint> FlatOffers;

        /// <summary>
        ///     The habbo club items
        /// </summary>
        internal List<CatalogItem> HabboClubItems;

        /// <summary>
        ///     The offers
        /// </summary>
        internal HybridDictionary Offers;

        /// <summary>
        ///     Checks the name of the pet.
        /// </summary>
        /// <param name="petName">Name of the pet.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool CheckPetName(string petName)
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
        internal static Pet CreatePet(uint userId, string name, string type, string race, string color, int rarity = 0)
        {
            uint trace = Convert.ToUInt32(race);

            uint petId = 404u;

            Pet pet = new Pet(petId, userId, 0u, name, type, trace, 0, 100, 150, 0, Yupi.GetUnixTimeStamp(), 0, 0, 0.0,
                false, 0, 0, -1, rarity, DateTime.Now.AddHours(36.0), DateTime.Now.AddHours(48.0), null, color)
            {
                DbState = DatabaseUpdateState.NeedsUpdate
            };

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "INSERT INTO pets_data (user_id, name, ai_type, pet_type, race_id, experience, energy, createstamp, rarity, lasthealth_stamp, untilgrown_stamp, color) " +
                    $"VALUES ('{pet.OwnerId}', @petName, 'pet', @petType, @petRace, 0, 100, UNIX_TIMESTAMP(), '{rarity}', UNIX_TIMESTAMP(now() + INTERVAL 36 HOUR), UNIX_TIMESTAMP(now() + INTERVAL 48 HOUR), @petColor)");

                commitableQueryReactor.AddParameter("petName", pet.Name);
                commitableQueryReactor.AddParameter("petType", pet.Type);
                commitableQueryReactor.AddParameter("petRace", pet.Race);
                commitableQueryReactor.AddParameter("petColor", pet.Color);

                petId = (uint)commitableQueryReactor.InsertQuery();
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
        internal static Pet GeneratePetFromRow(DataRow row)
        {
            MoplaBreed moplaBreed = null;

            if ((string) row["pet_type"] == "pet_monster")
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery(
                        $"SELECT * FROM pets_plants WHERE pet_id = {Convert.ToUInt32(row["id"])}");

                    DataRow sRow = commitableQueryReactor.GetRow();

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
        internal CatalogItem GetItemFromOffer(uint offerId)
        {
            CatalogItem result = null;

            if (FlatOffers.ContainsKey(offerId))
                result = (CatalogItem) Offers[FlatOffers[offerId]];

            return result ?? Yupi.GetGame().GetCatalog().GetItem(offerId);
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="pageLoaded">The page loaded.</param>
        internal void Initialize(IQueryAdapter dbClient, out int pageLoaded)
        {
            Initialize(dbClient);
            pageLoaded = Categories.Count;
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void Initialize(IQueryAdapter dbClient)
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
                    if (string.IsNullOrEmpty(dataRow["item_names"].ToString()) ||
                        string.IsNullOrEmpty(dataRow["amounts"].ToString()))
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
        internal CatalogItem GetItem(uint itemId) => Offers.Contains(itemId) ? (CatalogItem) Offers[itemId] : null;

        /// <summary>
        ///     Gets the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>CatalogPage.</returns>
        internal CatalogPage GetPage(uint page) => Categories.Contains(page) ? (CatalogPage) Categories[page] : null;

        /// <summary>
        ///     Handles the purchase.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="priceAmount">The price amount.</param>
        /// <param name="isGift">if set to <c>true</c> [is gift].</param>
        /// <param name="giftUser">The gift user.</param>
        /// <param name="giftMessage">The gift message.</param>
        /// <param name="giftSpriteId">The gift sprite identifier.</param>
        /// <param name="giftLazo">The gift lazo.</param>
        /// <param name="giftColor">Color of the gift.</param>
        /// <param name="undef">if set to <c>true</c> [undef].</param>
        /// <param name="theGroup">The theGroup.</param>
        internal void HandlePurchase(GameClient session, uint pageId, uint itemId, string extraData, uint priceAmount,
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

            if (catalogPage == null || !catalogPage.Enabled || !catalogPage.Visible || session?.GetHabbo() == null || catalogPage.MinRank > session.GetHabbo().Rank || catalogPage.Layout == "sold_ltd_items")
                return;

            CatalogItem item = catalogPage.GetItem(itemId);

            if (item == null || session.GetHabbo().Credits < item.CreditsCost || session.GetHabbo().Duckets < item.DucketsCost || session.GetHabbo().Diamonds < item.DiamondsCost || item.Name == "room_ad_plus_badge")
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

            #region Is Only for Habbo Club users Check

            if (item.ClubOnly && !session.GetHabbo().GetSubscriptionManager().HasSubscription)
            {
                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("CatalogPurchaseNotAllowedMessageComposer"));
                serverMessage.AppendInteger(1);
                session.SendMessage(serverMessage);
                return;
            }

            #endregion

            #region Limited Items Purchase

            if (item.IsLimited)
            {
                totalPrice = 1;
                priceAmount = 1;

                if (item.LimitedSelled >= item.LimitedStack)
                {
                    session.SendMessage(new ServerMessage(LibraryParser.OutgoingRequest("CatalogLimitedItemSoldOutMessageComposer")));
                    return;
                }

                item.LimitedSelled++;

                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.RunFastQuery(string.Concat("UPDATE catalog_items SET limited_sells = ",
                        item.LimitedSelled, " WHERE id = ", item.Id));

                    limitedId = item.LimitedSelled;
                    limtot = item.LimitedStack;
                }
            }
            else if (isGift && priceAmount > 1)
            {
                totalPrice = 1;
                priceAmount = 1;
            }

            #endregion

            uint toUserId = 0u;

            if (session.GetHabbo().Credits < item.CreditsCost*totalPrice || session.GetHabbo().Duckets < item.DucketsCost * totalPrice || session.GetHabbo().Diamonds < item.DiamondsCost * totalPrice)
                return;

            #region Decrease User Balance with Item Cost

            if (!isGift)
            {
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
            }

            #endregion

            foreach (Item baseItem in item.Items.Keys)
            {
                #region Stack if Gift

                if (isGift)
                {
                    if ((DateTime.Now - session.GetHabbo().LastGiftPurchaseTime).TotalSeconds <= 15.0)
                    {
                        session.SendNotif(Yupi.GetLanguage().GetVar("user_send_gift"));

                        return;
                    }

                    if (!baseItem.AllowGift)
                        return;

                    DataRow row;

                    using (IQueryAdapter queryreactor3 = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryreactor3.SetQuery("SELECT id FROM users WHERE username = @gift_user");
                        queryreactor3.AddParameter("gift_user", giftUser);
                        row = queryreactor3.GetRow();
                    }

                    if (row == null)
                    {
                        session.GetMessageHandler().GetResponse().Init(LibraryParser.OutgoingRequest("GiftErrorMessageComposer"));
                        session.GetMessageHandler().GetResponse().AppendString(giftUser);
                        session.GetMessageHandler().SendResponse();

                        return;
                    }

                    toUserId = (uint) row["id"];

                    if (toUserId == 0u)
                    {
                        session.GetMessageHandler().GetResponse().Init(LibraryParser.OutgoingRequest("GiftErrorMessageComposer"));
                        session.GetMessageHandler().GetResponse().AppendString(giftUser);
                        session.GetMessageHandler().SendResponse();

                        return;
                    }

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

                    if (baseItem.Type == 'e')
                    {
                        session.SendNotif(Yupi.GetLanguage().GetVar("user_send_gift_effect"));

                        return;
                    }
                }

                #endregion

                #region Item is Builders Club Addon

                if (item.Name.StartsWith("builders_club_addon_"))
                {
                    int furniAmount = int.Parse(item.Name.Replace("builders_club_addon_", string.Empty).Replace("furnis", string.Empty));

                    session.GetHabbo().BuildersItemsMax += furniAmount;

                    ServerMessage update = new ServerMessage(LibraryParser.OutgoingRequest("BuildersClubMembershipMessageComposer"));

                    update.AppendInteger(session.GetHabbo().BuildersExpire);
                    update.AppendInteger(session.GetHabbo().BuildersItemsMax);
                    update.AppendInteger(2);
                    session.SendMessage(update);

                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        commitableQueryReactor.SetQuery("UPDATE users SET builders_items_max = @max WHERE id = @userId");
                        commitableQueryReactor.AddParameter("max", session.GetHabbo().BuildersItemsMax);
                        commitableQueryReactor.AddParameter("userId", session.GetHabbo().Id);
                        commitableQueryReactor.RunQuery();
                    }

                    session.SendMessage(CatalogPageComposer.PurchaseOk(item, item.Items));
                    session.SendNotif("${notification.builders_club.membership_extended.message}", "${notification.builders_club.membership_extended.title}", "builders_club_membership_extended");

                    return;
                }

                #endregion

                #region Item is Builders Club Upgrade

                if (item.Name.StartsWith("builders_club_time_"))
                {
                    int timeAmount = int.Parse(item.Name.Replace("builders_club_time_", string.Empty).Replace("seconds", string.Empty));

                    session.GetHabbo().BuildersExpire += timeAmount;

                    ServerMessage update = new ServerMessage(LibraryParser.OutgoingRequest("BuildersClubMembershipMessageComposer"));

                    update.AppendInteger(session.GetHabbo().BuildersExpire);
                    update.AppendInteger(session.GetHabbo().BuildersItemsMax);
                    update.AppendInteger(2);
                    session.SendMessage(update);

                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        commitableQueryReactor.SetQuery("UPDATE users SET builders_expire = @max WHERE id = @userId");
                        commitableQueryReactor.AddParameter("max", session.GetHabbo().BuildersExpire);
                        commitableQueryReactor.AddParameter("userId", session.GetHabbo().Id);
                        commitableQueryReactor.RunQuery();
                    }

                    session.SendMessage(CatalogPageComposer.PurchaseOk(item, item.Items));
                    session.SendNotif("${notification.builders_club.membership_extended.message}", "${notification.builders_club.membership_extended.title}", "builders_club_membership_extended");

                    return;
                }

                #endregion

                string text = string.Empty;

                #region Interaction Type Selection

                Interaction interactionType = baseItem.InteractionType;

                switch (interactionType)
                {
                    case Interaction.None:
                    case Interaction.Gate:
                    case Interaction.Bed:
                    case Interaction.PressurePadBed:
                    case Interaction.Guillotine:
                    case Interaction.HcGate:
                    case Interaction.ScoreBoard:
                    case Interaction.VendingMachine:
                    case Interaction.Alert:
                    case Interaction.OneWayGate:
                    case Interaction.LoveShuffler:
                    case Interaction.HabboWheel:
                    case Interaction.Dice:
                    case Interaction.Bottle:
                    case Interaction.Hopper:
                    case Interaction.Teleport:
                    case Interaction.QuickTeleport:
                    case Interaction.Pet:
                    case Interaction.Pool:
                    case Interaction.Roller:
                        break;

                    case Interaction.PostIt:
                        extraData = "FFFF33";
                        break;

                    case Interaction.RoomEffect:
                        double number = string.IsNullOrEmpty(extraData) ? 0.0 : double.Parse(extraData, Yupi.CultureInfo);

                        extraData = number.ToString(CultureInfo.InvariantCulture).Replace(',', '.');
                        break;

                    case Interaction.Dimmer:
                        extraData = "1,1,1,#000000,255";
                        break;

                    case Interaction.Trophy:
                        extraData = string.Concat(session.GetHabbo().UserName, Convert.ToChar(9),
                            DateTime.Now.Day.ToString("00"), "-", DateTime.Now.Month.ToString("00"), "-",
                            DateTime.Now.Year, Convert.ToChar(9), extraData);
                        break;

                    case Interaction.Rentals:
                        extraData = item.ExtraData;
                        break;

                    case Interaction.PetDog:
                    case Interaction.PetCat:
                    case Interaction.PetCrocodile:
                    case Interaction.PetTerrier:
                    case Interaction.PetBear:
                    case Interaction.PetPig:
                    case Interaction.PetLion:
                    case Interaction.PetRhino:
                    case Interaction.PetSpider:
                    case Interaction.PetTurtle:
                    case Interaction.PetChick:
                    case Interaction.PetFrog:
                    case Interaction.PetDragon:
                    case Interaction.PetHorse:
                    case Interaction.PetMonkey:
                    case Interaction.PetGnomo:
                    case Interaction.PetMonsterPlant:
                    case Interaction.PetWhiteRabbit:
                    case Interaction.PetEvilRabbit:
                    case Interaction.PetLoveRabbit:
                    case Interaction.PetCafeRabbit:
                    case Interaction.PetPigeon:
                    case Interaction.PetEvilPigeon:
                    case Interaction.PetDemonMonkey:
                        string[] data = extraData.Split('\n');
                        string petName = data[0];
                        string race = data[1];
                        string color = data[2];

                        if (!CheckPetName(petName) || (race.Length != 1 && race.Length != 2) || color.Length != 6)
                            return;
                        break;

                    case Interaction.Mannequin:
                        extraData = string.Concat("m", Convert.ToChar(5), "ch-215-92.lg-3202-1322-73", Convert.ToChar(5), "Mannequin");
                        break;

                    case Interaction.VipGate:
                    case Interaction.MysteryBox:
                    case Interaction.YoutubeTv:
                    case Interaction.TileStackMagic:
                    case Interaction.Tent:
                    case Interaction.BedTent:
                        break;

                    case Interaction.BadgeDisplay:
                        if (!session.GetHabbo().GetBadgeComponent().HasBadge(extraData))
                            extraData = "UMAD";

                        extraData = $"{extraData}|{session.GetHabbo().UserName}|{DateTime.Now.Day.ToString("00")}-{DateTime.Now.Month.ToString("00")}-{DateTime.Now.Year}";
                        break;

                    case Interaction.FootballGate:
                        extraData = "hd-99999-99999.lg-270-62;hd-99999-99999.ch-630-62.lg-695-62";
                        break;

                    case Interaction.LoveLock:
                        extraData = "0";
                        break;

                    case Interaction.Pinata:
                    case Interaction.RunWaySage:
                    case Interaction.Shower:
                        extraData = "0";
                        break;

                    case Interaction.GroupForumTerminal:
                    case Interaction.GuildItem:
                    case Interaction.GuildGate:
                    case Interaction.GuildForum:
                    case Interaction.Poster:
                        break;

                    case Interaction.Moplaseed:
                        extraData = new Random().Next(0, 12).ToString();
                        break;

                    case Interaction.RareMoplaSeed:
                        extraData = new Random().Next(10, 12).ToString();
                        break;

                    case Interaction.MusicDisc:

                        SongData song = SoundMachineSongManager.GetSongById(item.SongId);

                        extraData = string.Empty;

                        if (song == null)
                            break;

                        extraData = string.Concat(session.GetHabbo().UserName, '\n', DateTime.Now.Year, '\n',
                            DateTime.Now.Month, '\n', DateTime.Now.Day, '\n', song.LengthSeconds, '\n', song.Name);

                        text = song.CodeName;

                        break;

                    default:
                        extraData = item.ExtraData;
                        break;
                }

                #endregion

                session.GetMessageHandler().GetResponse().Init(LibraryParser.OutgoingRequest("UpdateInventoryMessageComposer"));

                session.GetMessageHandler().SendResponse();

                session.SendMessage(CatalogPageComposer.PurchaseOk(item, item.Items));

                if (isGift)
                {
                    Item itemBySprite = Yupi.GetGame().GetItemManager().GetItemBySpriteId(giftSpriteId);

                    if (itemBySprite == null)
                        return;

                    uint insertId;

                    using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        commitableQueryReactor.SetQuery("INSERT INTO items_rooms (item_name,user_id) VALUES (" + itemBySprite.Name + ", " + toUserId + ")");

                        insertId = (uint) commitableQueryReactor.InsertQuery();

                        commitableQueryReactor.SetQuery(
                            string.Concat(
                                "INSERT INTO users_gifts (gift_id,item_id,extradata,giver_name,Message,ribbon,color,gift_sprite,show_sender,rare_id) VALUES (",
                                insertId, ", ", baseItem.ItemId, ",@extradata, @name, @Message,", giftLazo, ",",
                                giftColor, ",", giftSpriteId, ",", undef ? 1 : 0, ",", limitedId, ")"));
                        commitableQueryReactor.AddParameter("extradata", extraData);
                        commitableQueryReactor.AddParameter("name", giftUser);
                        commitableQueryReactor.AddParameter("message", giftMessage);
                        commitableQueryReactor.RunQuery();

                        if (session.GetHabbo().Id != toUserId)
                        {
                            Yupi.GetGame()
                                .GetAchievementManager()
                                .ProgressUserAchievement(session, "ACH_GiftGiver", 1, true);

                            commitableQueryReactor.RunFastQuery(
                                "UPDATE users_stats SET gifts_given = gifts_given + 1 WHERE id = " +
                                session.GetHabbo().Id +
                                ";UPDATE users_stats SET gifts_received = gifts_received + 1 WHERE id = " + toUserId);
                        }
                    }

                    GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(toUserId);

                    if (clientByUserId != null)
                    {
                        clientByUserId.GetHabbo()
                            .GetInventoryComponent()
                            .AddNewItem(insertId, itemBySprite.Name,
                                string.Concat(session.GetHabbo().Id, (char) 9, giftMessage, (char) 9, giftLazo, (char) 9,
                                    giftColor, (char) 9, undef ? "1" : "0", (char) 9, session.GetHabbo().UserName,
                                    (char) 9, session.GetHabbo().Look, (char) 9, item.Name), 0u, false, false, 0, 0);

                        if (clientByUserId.GetHabbo().Id != session.GetHabbo().Id)
                            Yupi.GetGame()
                                .GetAchievementManager()
                                .ProgressUserAchievement(clientByUserId, "ACH_GiftReceiver", 1, true);
                    }

                    session.GetHabbo().LastGiftPurchaseTime = DateTime.Now;

                    continue;
                }

                session.GetMessageHandler()
                    .GetResponse()
                    .Init(LibraryParser.OutgoingRequest("NewInventoryObjectMessageComposer"));

                session.GetMessageHandler().GetResponse().AppendInteger(1);

                int i = 1;

                if (baseItem.Type == 's')
                    i = InteractionTypes.AreFamiliar(GlobalInteractions.Pet, baseItem.InteractionType) ? 3 : 1;

                session.GetMessageHandler().GetResponse().AppendInteger(i);

                List<UserItem> list = DeliverItems(session, baseItem, priceAmount*item.Items[baseItem], extraData, limitedId,
                    limtot, text);

                session.GetMessageHandler().GetResponse().AppendInteger(list.Count);

                foreach (UserItem current3 in list)
                    session.GetMessageHandler().GetResponse().AppendInteger(current3.Id);

                session.GetMessageHandler().SendResponse();

                session.GetHabbo().GetInventoryComponent().UpdateItems(false);

                if (InteractionTypes.AreFamiliar(GlobalInteractions.Pet, baseItem.InteractionType))
                    session.SendMessage(session.GetHabbo().GetInventoryComponent().SerializePetInventory());
            }

            if (item.Badge.Length >= 1)
                session.GetHabbo().GetBadgeComponent().GiveBadge(item.Badge, true, session);
        }

        /// <summary>
        ///     Delivers the items.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="item">The item.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="limno">The limno.</param>
        /// <param name="limtot">The limtot.</param>
        /// <param name="songCode">The song code.</param>
        /// <returns>List&lt;UserItem&gt;.</returns>
        internal List<UserItem> DeliverItems(GameClient session, Item item, uint amount, string extraData, uint limno,
            uint limtot, string songCode)
        {
            List<UserItem> list = new List<UserItem>();

            if (item.InteractionType == Interaction.PostIt)
                amount = amount*20;

            char a = item.Type;
            switch (a)
            {
                case 'i':
                case 's':
                    int i = 0;

                    while (i < amount)
                    {
                        Interaction interactionType = item.InteractionType;

                        switch (interactionType)
                        {
                            case Interaction.Dimmer:
                                UserItem userItem33 = session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, extraData, 0u, true, false, 0, 0);
                                uint id33 = userItem33.Id;

                                list.Add(userItem33);

                                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                                    queryreactor2.RunFastQuery(
                                        $"INSERT INTO items_moodlight (item_id,enabled,current_preset,preset_one,preset_two,preset_three) VALUES ({id33},'0',1,'#000000,255,0','#000000,255,0','#000000,255,0')");

                                break;

                            case Interaction.Trophy:
                            case Interaction.Bed:
                            case Interaction.PressurePadBed:
                            case Interaction.Guillotine:
                            case Interaction.ScoreBoard:
                            case Interaction.VendingMachine:
                            case Interaction.Alert:
                            case Interaction.OneWayGate:
                            case Interaction.LoveShuffler:
                            case Interaction.HabboWheel:
                            case Interaction.Dice:
                            case Interaction.Bottle:
                            case Interaction.Hopper:
                            case Interaction.Rentals:
                            case Interaction.Pet:
                            case Interaction.Pool:
                            case Interaction.Roller:
                            case Interaction.FootballGate:
                                list.Add(session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, extraData, 0u, true, false, limno, limtot));
                                break;

                            case Interaction.Teleport:
                            case Interaction.QuickTeleport:
                                UserItem userItem = session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, "0", 0u, true, false, 0, 0);
                                uint id = userItem.Id;
                                UserItem userItem2 = session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, "0", 0u, true, false, 0, 0);
                                uint id2 = userItem2.Id;

                                list.Add(userItem);
                                list.Add(userItem2);

                                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                                    commitableQueryReactor.RunFastQuery(
                                        $"INSERT INTO items_teleports (tele_one_id,tele_two_id) VALUES ('{id}','{id2}');" +
                                        $"INSERT INTO items_teleports (tele_one_id,tele_two_id) VALUES ('{id2}','{id}')");

                                break;

                            case Interaction.PetDog:
                            case Interaction.PetCat:
                            case Interaction.PetCrocodile:
                            case Interaction.PetTerrier:
                            case Interaction.PetBear:
                            case Interaction.PetPig:
                            case Interaction.PetLion:
                            case Interaction.PetRhino:
                            case Interaction.PetSpider:
                            case Interaction.PetTurtle:
                            case Interaction.PetChick:
                            case Interaction.PetFrog:
                            case Interaction.PetDragon:
                            case Interaction.PetHorse:
                            case Interaction.PetMonkey:
                            case Interaction.PetGnomo:
                            case Interaction.PetMonsterPlant:
                            case Interaction.PetWhiteRabbit:
                            case Interaction.PetEvilRabbit:
                            case Interaction.PetLoveRabbit:
                            case Interaction.PetPigeon:
                            case Interaction.PetEvilPigeon:
                            case Interaction.PetDemonMonkey:
                                string[] petData = extraData.Split('\n');

                                Pet generatedPet = CreatePet(session.GetHabbo().Id, petData[0], item.Name, petData[1],
                                    petData[2]);

                                session.GetHabbo().GetInventoryComponent().AddPet(generatedPet);
                                break;

                            case Interaction.MusicDisc:
                                list.Add(session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, extraData, 0u, true, false, 0, 0, songCode));
                                break;

                            case Interaction.PuzzleBox:
                                list.Add(session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, extraData, 0u, true, false, limno, limtot));
                                break;

                            case Interaction.RoomBg:
                                UserItem userItem44 = session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, extraData, 0u, true, false, 0, 0, string.Empty);
                                uint id44 = userItem44.Id;

                                list.Add(userItem44);

                                using (IQueryAdapter queryreactor3 = Yupi.GetDatabaseManager().GetQueryReactor())
                                    queryreactor3.RunFastQuery($"INSERT INTO items_toners VALUES ({id44},'0',0,0,0)");

                                break;

                            case Interaction.GuildItem:
                            case Interaction.GuildGate:
                            case Interaction.GroupForumTerminal:
                                list.Add(session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, "0", Convert.ToUInt32(extraData), true, false, 0, 0,
                                        string.Empty));
                                break;

                            case Interaction.GuildForum:
                                uint groupId;

                                uint.TryParse(extraData, out groupId);

                                Group group = Yupi.GetGame().GetGroupManager().GetGroup(groupId);

                                if (group != null)
                                {
                                    if (group.CreatorId == session.GetHabbo().Id)
                                        group.UpdateForum(true);
                                    else
                                        session.SendNotif(Yupi.GetLanguage().GetVar("user_group_owner_error"));
                                }

                                list.Add(session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, "0", Convert.ToUInt32(extraData), true, false, 0, 0,
                                        string.Empty));
                                break;

                            default:
                                list.Add(session.GetHabbo()
                                    .GetInventoryComponent()
                                    .AddNewItem(0u, item.Name, extraData, 0u, true, false, limno, limtot));
                                break;
                        }

                        i++;
                    }

                    return list;
                case 'e':
                    for (int j = 0; j < amount; j++)
                        session.GetHabbo().GetAvatarEffectsInventoryComponent().AddNewEffect(item.SpriteId, 7200, 0);
                    break;
                case 'r':
                    RoomBot bot = BotManager.CreateBotFromCatalog(item.Name, session.GetHabbo().Id);
                    session.GetHabbo().GetInventoryComponent().AddBot(bot);
                    session.SendMessage(session.GetHabbo().GetInventoryComponent().SerializeBotInventory());
                    break;
            }

            return list;
        }

        /// <summary>
        ///     Gets the random ecotron reward.
        /// </summary>
        /// <returns>EcotronReward.</returns>
        internal EcotronReward GetRandomEcotronReward()
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
        internal List<EcotronReward> GetEcotronRewards()
        {
            return EcotronRewards;
        }

        /// <summary>
        ///     Gets the ecotron rewards levels.
        /// </summary>
        /// <returns>List&lt;System.Int32&gt;.</returns>
        internal List<int> GetEcotronRewardsLevels()
        {
            return EcotronLevels;
        }

        /// <summary>
        ///     Gets the ecotron rewards for level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>List&lt;EcotronReward&gt;.</returns>
        internal List<EcotronReward> GetEcotronRewardsForLevel(uint level)
        {
            return EcotronRewards.Where(current => current.RewardLevel == level).ToList();
        }
    }
}