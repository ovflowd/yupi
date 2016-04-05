using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Yupi.Emulator.Core.Algorithms.Encryption;
using Yupi.Emulator.Core.Settings;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser.Composers;
using Yupi.Emulator.Game.Catalogs.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Net.Web;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class MessageHandler.
    /// </summary>
     partial class MessageHandler
    {
        /// <summary>
        ///     Pongs this instance.
        /// </summary>
         void Pong()
        {
            Session.TimePingedReceived = DateTime.Now;
        }

        /// <summary>
        ///     Disconnects the event.
        /// </summary>
         void DisconnectEvent() => Session.Disconnect("Closed Client Page.", true);

        /// <summary>
        ///     Latencies the test.
        /// </summary>
         void LatencyTest()
        {
            if (Session == null)
                return;

            Yupi.GetGame()?.GetAchievementManager()?.ProgressUserAchievement(Session, "ACH_AllTimeHotelPresence", 1, true);

            if (Session.GetHabbo() == null)
                return;

            Session.TimePingedReceived = DateTime.Now;
        }

        /// <summary>
        ///     Initializes the crypto.
        /// </summary>
         void InitCrypto()
        {
            Response.Init(PacketLibraryManager.OutgoingHandler("InitCryptoMessageComposer"));

            Response.AppendString("Yupi");
            Response.AppendString("Disabled Crypto");
            SendResponse();
        }

        /// <summary>
        ///     Secrets the key.
        /// </summary>
         void SecretKey()
        {
            Request.GetString();

            Response.Init(PacketLibraryManager.OutgoingHandler("SecretKeyMessageComposer"));

            Response.AppendString("Crypto disabled");
            Response.AppendBool(false);
            SendResponse();
        }

        /// <summary>
        ///     Machines the identifier.
        /// </summary>
         void MachineId()
        {
            Request.GetString();

            string machineId = Request.GetString();

            Session.MachineId = machineId;
        }

        /// <summary>
        ///     Logins the with ticket.
        /// </summary>
         void LoginWithTicket()
        {
            if (Session == null || Session.GetHabbo() != null)
                return;

            string banReason;
			// TODO Don't compare to false
            if (Session.TryLogin(Request.GetString(), out banReason) == false)
                Session.Disconnect($"Banned from Server. Reason: {banReason}.", true);

            if (Session != null)
                Session.TimePingedReceived = DateTime.Now;
        }

        /// <summary>
        ///     Informations the retrieve.
        /// </summary>
         void InfoRetrieve()
        {
            if (Session?.GetHabbo() == null)
                return;

            Habbo habbo = Session.GetHabbo();

            bool tradeLocked = Session.GetHabbo().CheckTrading();

            Response.Init(PacketLibraryManager.OutgoingHandler("UserObjectMessageComposer"));

            Response.AppendInteger(habbo.Id);
            Response.AppendString(habbo.UserName);
            Response.AppendString(habbo.Look);
            Response.AppendString(habbo.Gender.ToUpper());
            Response.AppendString(habbo.Motto);
            Response.AppendString(string.Empty);
            Response.AppendBool(false);
            Response.AppendInteger(habbo.Respect);
            Response.AppendInteger(habbo.DailyRespectPoints);
            Response.AppendInteger(habbo.DailyPetRespectPoints);
            Response.AppendBool(true);
            Response.AppendString(habbo.LastOnline.ToString(CultureInfo.InvariantCulture));
            Response.AppendBool(habbo.CanChangeName);
            Response.AppendBool(false);
            SendResponse();

            Response.Init(PacketLibraryManager.OutgoingHandler("BuildersClubMembershipMessageComposer"));
            Response.AppendInteger(Session.GetHabbo().BuildersExpire);
            Response.AppendInteger(Session.GetHabbo().BuildersItemsMax);
            Response.AppendInteger(2);
            SendResponse();

            Response.Init(PacketLibraryManager.OutgoingHandler("SendPerkAllowancesMessageComposer"));
            Response.AppendInteger(11);

            Response.AppendString("BUILDER_AT_WORK");
            Response.AppendString(string.Empty);
            Response.AppendBool(true);

            Response.AppendString("VOTE_IN_COMPETITIONS");
            Response.AppendString("requirement.unfulfilled.helper_level_2");
            Response.AppendBool(false);

            Response.AppendString("USE_GUIDE_TOOL");
            Response.AppendString((Session.GetHabbo().TalentStatus == "helper" && Session.GetHabbo().CurrentTalentLevel >= 4) || (Session.GetHabbo().Rank >= 4) ? string.Empty : "requirement.unfulfilled.helper_level_4");
            Response.AppendBool((Session.GetHabbo().TalentStatus == "helper" && Session.GetHabbo().CurrentTalentLevel >= 4) || (Session.GetHabbo().Rank >= 4));

            Response.AppendString("JUDGE_CHAT_REVIEWS");
            Response.AppendString("requirement.unfulfilled.helper_level_6");
            Response.AppendBool(false);

            Response.AppendString("NAVIGATOR_ROOM_THUMBNAIL_CAMERA");
            Response.AppendString(string.Empty);
            Response.AppendBool(true);

            Response.AppendString("CALL_ON_HELPERS");
            Response.AppendString(string.Empty);
            Response.AppendBool(true);

            Response.AppendString("CITIZEN");
            Response.AppendString(string.Empty);
            Response.AppendBool(Session.GetHabbo().TalentStatus == "helper" || Session.GetHabbo().CurrentTalentLevel >= 4);

            Response.AppendString("MOUSE_ZOOM");
            Response.AppendString(string.Empty);
            Response.AppendBool(false);

            Response.AppendString("TRADE");
            Response.AppendString(tradeLocked ? string.Empty : "requirement.unfulfilled.no_trade_lock");
            Response.AppendBool(tradeLocked);

            Response.AppendString("CAMERA");
            Response.AppendString(string.Empty);
            Response.AppendBool(ServerExtraSettings.EnableBetaCamera);

            Response.AppendString("NAVIGATOR_PHASE_TWO_2014");
            Response.AppendString(string.Empty);
            Response.AppendBool(true);

            SendResponse();

            Session.GetHabbo().InitMessenger();

            GetResponse().Init(PacketLibraryManager.OutgoingHandler("CitizenshipStatusMessageComposer"));
            GetResponse().AppendString("citizenship");
            GetResponse().AppendInteger(1);
            GetResponse().AppendInteger(4);
            SendResponse();

            GetResponse().Init(PacketLibraryManager.OutgoingHandler("GameCenterGamesListMessageComposer"));
            GetResponse().AppendInteger(1);
            GetResponse().AppendInteger(18);
            GetResponse().AppendString("elisa_habbo_stories");
            GetResponse().AppendString("000000");
            GetResponse().AppendString("ffffff");
            GetResponse().AppendString("");
            GetResponse().AppendString("");
            SendResponse();

            GetResponse().Init(PacketLibraryManager.OutgoingHandler("AchievementPointsMessageComposer"));
            GetResponse().AppendInteger(Session.GetHabbo().AchievementPoints);
            SendResponse();

            GetResponse().Init(PacketLibraryManager.OutgoingHandler("FigureSetIdsMessageComposer"));
            Session.GetHabbo().ClothesManagerManager.Serialize(GetResponse());
            SendResponse();

            Session.SendMessage(PromotionCategoriesListComposer.Compose());

            if (Yupi.GetGame().GetTargetedOfferManager().CurrentOffer != null)
            {
                Yupi.GetGame().GetTargetedOfferManager().GenerateMessage(GetResponse());

                SendResponse();
            }
        }

        /// <summary>
        ///     Habboes the camera.
        /// </summary>
         void HabboCamera()
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM cms_stories_photos_preview WHERE user_id = {Session.GetHabbo().Id} AND type = 'PHOTO' ORDER BY id DESC LIMIT 1");

                DataTable table = queryReactor.GetTable();

                foreach (DataRow dataRow in table.Rows)
                {
                    object date = dataRow["date"];
                    object room = dataRow["room_id"];
                    object photo = dataRow["id"];
                    object image = dataRow["image_url"];

                    using (IQueryAdapter queryReactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    {
                        queryReactor2.SetQuery(
                            "INSERT INTO cms_stories_photos (user_id,user_name,room_id,image_preview_url,image_url,type,date,tags) VALUES (@user_id,@user_name,@room_id,@image_url,@image_url,@type,@date,@tags)");
                        queryReactor2.AddParameter("user_id", Session.GetHabbo().Id);
                        queryReactor2.AddParameter("user_name", Session.GetHabbo().UserName);
                        queryReactor2.AddParameter("room_id", room);
                        queryReactor2.AddParameter("image_url", image);
                        queryReactor2.AddParameter("type", "PHOTO");
                        queryReactor2.AddParameter("date", date);
                        queryReactor2.AddParameter("tags", "");
                        queryReactor2.RunQuery();

                        string newPhotoData = "{\"t\":" + date + ",\"u\":\"" + photo + "\",\"m\":\"\",\"s\":" + room +
                                           ",\"w\":\"" + image + "\"}";

                        UserItem item = Session.GetHabbo()
                            .GetInventoryComponent()
                            .AddNewItem(0, "external_image_wallitem_poster", newPhotoData, 0, true, false, 0, 0);

                        Session.GetHabbo().GetInventoryComponent().UpdateItems(false);
                        Session.GetHabbo().Credits -= 2;
                        Session.GetHabbo().UpdateCreditsBalance();
                        Session.GetHabbo().GetInventoryComponent().SendNewItems(item.Id);
                    }
                }
            }

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("CameraPurchaseOk"));

            Session.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     Targeteds the offer buy.
        /// </summary>
         void PurchaseTargetedOffer()
        {
            Request.GetUInt32();
            uint quantity = Request.GetUInt32();

            TargetedOffer offer = Yupi.GetGame().GetTargetedOfferManager().CurrentOffer;

            if (offer == null)
                return;

            if (Session.GetHabbo().Credits < offer.CostCredits * quantity)
                return;

            if (Session.GetHabbo().Duckets < offer.CostDuckets * quantity)
                return;

            if (Session.GetHabbo().Diamonds < offer.CostDiamonds * quantity)
                return;

            foreach (string product in offer.Products)
            {
                Item item = Yupi.GetGame().GetItemManager().GetItemByName(product);

                if (item == null)
                    continue;

                Yupi.GetGame()
                    .GetCatalogManager()
                    .DeliverItems(Session, item, quantity, string.Empty, 0, 0, string.Empty);
            }

            Session.GetHabbo().Credits -= offer.CostCredits * quantity;
            Session.GetHabbo().Duckets -= offer.CostDuckets * quantity;
            Session.GetHabbo().Diamonds -= offer.CostDiamonds * quantity;
            Session.GetHabbo().UpdateCreditsBalance();
            Session.GetHabbo().UpdateSeasonalCurrencyBalance();
            Session.GetHabbo().GetInventoryComponent().UpdateItems(false);
        }

        /// <summary>
        ///     Goes the name of to room by.
        /// </summary>
         void GoToRoomByName()
        {
            string name = Request.GetString();
            uint roomId = 0;

            switch (name)
            {
                case "predefined_noob_lobby":
                    {
                        roomId = Convert.ToUInt32(Yupi.GetDbConfig().DbData["noob.lobby.roomid"]);

                        break;
                    }
                case "random_friending_room":
                    {
                        if (Yupi.GetGame().GetRoomManager().GetActiveRooms() == null)
                            return;

                        List<RoomData> rooms = Yupi.GetGame().GetRoomManager().GetActiveRooms().Select(room => room.Key).Where(room => room != null && room.UsersNow > 0).ToList();

                        if (!rooms.Any())
                            return;

                        if (rooms.Count == 1)
                        {
                            roomId = rooms.First().Id;

                            break;
                        }

                        roomId = rooms[Yupi.GetRandomNumber(0, rooms.Count)].Id;

                        break;
                    }
            }

            if (roomId == 0)
                return;

            SimpleServerMessageBuffer roomFwd = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomForwardMessageComposer"));

            roomFwd.AppendInteger(roomId);

            Session.SendMessage(roomFwd);
        }

        /// <summary>
        ///     Saves the room thumbnail.
        /// </summary>
         void SaveRoomThumbnail()
        {
            try
            {
                int count = Request.GetInteger();

                byte[] bytes = Request.GetBytes(count);

                string outData = Converter.Deflate(bytes);

                WebManager.HttpPostJson(ServerExtraSettings.StoriesApiThumbnailServerUrl, outData);

                SimpleServerMessageBuffer thumb = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("ThumbnailSuccessMessageComposer"));

                thumb.AppendBool(true);
                thumb.AppendBool(false);

                Session.SendMessage(thumb);
            }
            catch
            {
                Session.SendNotif("Please Try Again. This Area has too many elements.");
            }
        }
    }
}