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
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser.Enums;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser
{
    /// <summary>
    ///     Class NavigatorManager.
    /// </summary>
    internal class HotelBrowserManager
    {
        /// <summary>
        ///     The _public items
        /// </summary>
        public readonly Dictionary<uint, PublicItem> PublicItems;

        /// <summary>
        ///     The _navigator headers
        /// </summary>
        public readonly List<NavigatorHeader> NavigatorHeaders;

        /// <summary>
        ///     The in categories
        /// </summary>
        internal Dictionary<string, NavigatorCategory> InCategories;

        /// <summary>
        ///     The new public rooms
        /// </summary>
        internal SimpleServerMessageBuffer NewPublicRooms, NewStaffPicks;

        /// <summary>
        ///     The private categories
        /// </summary>
        internal HybridDictionary PrivateCategories;

        /// <summary>
        ///     The promo categories
        /// </summary>
        internal Dictionary<int, PromoCategory> PromoCategories;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelBrowserManager" /> class.
        /// </summary>
        internal HotelBrowserManager()
        {
            PrivateCategories = new HybridDictionary();

            InCategories = new Dictionary<string, NavigatorCategory>();
            PublicItems = new Dictionary<uint, PublicItem>();

            NavigatorHeaders = new List<NavigatorHeader>();

            PromoCategories = new Dictionary<int, PromoCategory>();
        }

        /// <summary>
        ///     Get the Number of Flat Caegories
        /// </summary>
        /// <value>The flat cats count.</value>
        internal int FlatCatsCount => PrivateCategories.Count;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        /// <param name="navLoaded">The nav loaded.</param>
        public void Initialize(IQueryAdapter dbClient, out uint navLoaded)
        {
            Initialize(dbClient);

            navLoaded = (uint) NavigatorHeaders.Count;
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        public void Initialize(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM navigator_flatcats WHERE enabled = '2'");
            DataTable navigatorFlatCats = dbClient.GetTable();

            dbClient.SetQuery("SELECT * FROM navigator_publics");
            DataTable navigatorPublicRooms = dbClient.GetTable();

            dbClient.SetQuery("SELECT * FROM navigator_promocats");
            DataTable navigatorPromoCats = dbClient.GetTable();

            if (navigatorPromoCats != null)
            {
                PromoCategories.Clear();

                foreach (DataRow dataRow in navigatorPromoCats.Rows)
                    PromoCategories.Add((int) dataRow["id"],
                        new PromoCategory((int) dataRow["id"], (string) dataRow["caption"], (int) dataRow["min_rank"],
                            Yupi.EnumToBool((string) dataRow["visible"])));
            }

            if (navigatorFlatCats != null)
            {
                PrivateCategories.Clear();

                foreach (DataRow dataRow in navigatorFlatCats.Rows)
                    PrivateCategories.Add((int) dataRow["id"],
                        new PublicCategory((int) dataRow["id"], (string) dataRow["caption"], (int) dataRow["min_rank"]));
            }

            if (navigatorPublicRooms != null)
            {
                PublicItems.Clear();

                foreach (DataRow row in navigatorPublicRooms.Rows)
                    PublicItems.Add(Convert.ToUInt32(row["id"]),
                        new PublicItem(Convert.ToUInt32(row["id"]), int.Parse(row["bannertype"].ToString()),
                            (string) row["caption"],
                            (string) row["description"], (string) row["image"],
                            row["image_type"].ToString().ToLower() == "internal"
                                ? PublicImageType.Internal
                                : PublicImageType.External, (uint) row["room_id"], 0, (int) row["category_parent_id"],
                            row["recommended"].ToString() == "1", (int) row["typeofdata"]));
            }

            InitializeCategories();
        }

        public void InitializeCategories()
        {
            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT * FROM navigator_pubcats");
                DataTable navigatorPublicCats = dbClient.GetTable();

                dbClient.SetQuery("SELECT * FROM navigator_sub_pubcats");
                DataTable navigatorSubCats = dbClient.GetTable();

                List<NavigatorSubCategory> subCategories = new List<NavigatorSubCategory>();

                if (navigatorSubCats != null)
                    subCategories.AddRange(from DataRow dataRow in navigatorSubCats.Rows select new NavigatorSubCategory((int)dataRow["id"], (string)dataRow["caption"], (string)dataRow["main_cat"], (string)dataRow["default_state"] == "opened", (string)dataRow["default_size"] == "image"));

                if (navigatorPublicCats != null)
                {
                    InCategories.Clear();

                    foreach (DataRow dataRow in navigatorPublicCats.Rows)
                        InCategories.Add((string)dataRow["caption"], new NavigatorCategory((int)dataRow["id"], (string)dataRow["caption"], (string)dataRow["default_state"] == "opened", (string)dataRow["default_size"] == "image", subCategories.Where(c => c.MainCategory == (string)dataRow["caption"]).ToList()));
                }
            }
        }

        public void AddPublicItem(PublicItem item)
        {
            if (item == null)
                return;

            PublicItems.Add(Convert.ToUInt32(item.Id), item);
        }

        public void RemovePublicItem(uint id)
        {
            if (!PublicItems.ContainsKey(id))
                return;

            PublicItems.Remove(id);
        }

        /// <summary>
        ///     Loads the new public rooms.
        /// </summary>
        public void LoadNewPublicRooms()
        {
            NewPublicRooms = SerializeNewPublicRooms();

            NewStaffPicks = SerializeNewStaffPicks();
        }

        /// <summary>
        ///     Serializes the navigator popular rooms news.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="rooms">The rooms.</param>
        /// <param name="category">The category.</param>
        /// <param name="direct">if set to <c>true</c> [direct].</param>
        public void SerializeNavigatorPopularRoomsNews(ref SimpleServerMessageBuffer reply, KeyValuePair<RoomData, uint>[] rooms, int category, bool direct)
        {
            if (!rooms?.Any() ?? true)
            {
                reply.AppendInteger(0);

                return;
            }

            List<RoomData> roomsCategory = new List<RoomData>();

            if (rooms != null)
            {
                foreach (KeyValuePair<RoomData, uint> pair in rooms)
                {
                    if (pair.Key.Category.Equals(category))
                    {
                        roomsCategory.Add(pair.Key);

                        if (roomsCategory.Count == (direct ? 40 : 8))
                            break;
                    }
                }
            }

            reply.AppendInteger(roomsCategory.Count);

            foreach (RoomData data in roomsCategory)
                data.Serialize(reply);
        }

        /// <summary>
        ///     Serializes the promotion categories.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer SerializePromotionCategories()
        {
            SimpleServerMessageBuffer categories =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("CatalogPromotionGetCategoriesMessageComposer"));

            categories.AppendInteger(PromoCategories.Count);

            foreach (PromoCategory cat in PromoCategories.Values)
            {
                categories.AppendInteger(cat.Id);
                categories.AppendString(cat.Caption);
                categories.AppendBool(cat.Visible);
            }

            return categories;
        }

        /// <summary>
        ///     Serializes the new public rooms.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer SerializeNewPublicRooms()
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();

            messageBuffer.StartArray();

            foreach (PublicItem item in PublicItems.Values)
            {
                if (item.ParentId == -1)
                {
                    messageBuffer.Clear();

                    if (item.GetPublicRoomData == null)
                        continue;

                    item.GetPublicRoomData.Serialize(messageBuffer);

                    messageBuffer.SaveArray();
                }
            }

            messageBuffer.EndArray();

            return messageBuffer;
        }

        internal SimpleServerMessageBuffer SerializeNewStaffPicks()
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();

            messageBuffer.StartArray();

            foreach (PublicItem item in PublicItems.Values.Where(t => t.ParentId == -2))
            {
                messageBuffer.Clear();

                if (item.GetPublicRoomData == null)
                    continue;

                item.GetPublicRoomData.Serialize(messageBuffer);
                messageBuffer.SaveArray();
            }

            messageBuffer.EndArray();

            return messageBuffer;
        }

        /// <summary>
        ///     Gets the flat cat.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>FlatCat.</returns>
        internal PublicCategory GetFlatCat(int id)
            => PrivateCategories.Contains(id) ? (PublicCategory) PrivateCategories[id] : null;

        /// <summary>
        ///     Serializes the new flat categories.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer SerializeNewFlatCategories()
        {
            List<PublicCategory> flatcat = Yupi.GetGame().GetNavigator().PrivateCategories.OfType<PublicCategory>().ToList();

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorNewFlatCategoriesMessageComposer"));

            messageBuffer.AppendInteger(flatcat.Count);

            foreach (PublicCategory cat in flatcat)
            {
                messageBuffer.AppendInteger(cat.Id);
                messageBuffer.AppendInteger(cat.UsersNow);
                messageBuffer.AppendInteger(500);
            }

            return messageBuffer;
        }

        /// <summary>
        ///     Serlializes the new navigator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="session">The session.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer SerializeNewNavigator(string value1, string value2, GameClient session)
        {
            SimpleServerMessageBuffer newNavigator = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("SearchResultSetComposer"));

            newNavigator.AppendString(value1);
            newNavigator.AppendString(value2);
            newNavigator.AppendInteger(value2.Length > 0 ? 1 : GetNewNavigatorLength(value1));

            if (value2.Length > 0)
                SearchResultList.SerializeSearches(value2, newNavigator, session);
            else
                SearchResultList.SerializeSearchResultListStatics(value1, true, newNavigator, session);

            return newNavigator;
        }

        /// <summary>
        ///     Initializes Navigator
        /// </summary>
        /// <param name="session">The session.</param>
        internal void InitializeNavigator(GameClient session)
        {
            SimpleServerMessageBuffer navigatorMetaDataParser = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorMetaDataComposer"));

            navigatorMetaDataParser.AppendInteger(4);
            navigatorMetaDataParser.AppendString("official_view");
            navigatorMetaDataParser.AppendInteger(0);
            navigatorMetaDataParser.AppendString("hotel_view");
            navigatorMetaDataParser.AppendInteger(0);
            navigatorMetaDataParser.AppendString("roomads_view");
            navigatorMetaDataParser.AppendInteger(0);
            navigatorMetaDataParser.AppendString("myworld_view");
            navigatorMetaDataParser.AppendInteger(0);
            session.SendMessage(navigatorMetaDataParser);

            SimpleServerMessageBuffer navigatorLiftedRoomsParser = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorLiftedRoomsComposer"));

            navigatorLiftedRoomsParser.AppendInteger(NavigatorHeaders.Count);

            foreach (NavigatorHeader navHeader in NavigatorHeaders)
            {
                navigatorLiftedRoomsParser.AppendInteger(navHeader.RoomId);
                navigatorLiftedRoomsParser.AppendInteger(0);
                navigatorLiftedRoomsParser.AppendString(navHeader.Image);
                navigatorLiftedRoomsParser.AppendString(navHeader.Caption);
            }

            session.SendMessage(navigatorLiftedRoomsParser);

            SimpleServerMessageBuffer collapsedCategoriesMessageBufferParser = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorCategorys"));

            collapsedCategoriesMessageBufferParser.AppendInteger(FlatCatsCount + 4);

            foreach (PublicCategory flat in PrivateCategories.Values)
                collapsedCategoriesMessageBufferParser.AppendString($"category__{flat.Caption}");

            collapsedCategoriesMessageBufferParser.AppendString("recommended");
            collapsedCategoriesMessageBufferParser.AppendString("new_ads");
            collapsedCategoriesMessageBufferParser.AppendString("staffpicks");
            collapsedCategoriesMessageBufferParser.AppendString("official");
            session.SendMessage(collapsedCategoriesMessageBufferParser);

            SimpleServerMessageBuffer searches = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorSavedSearchesComposer"));

            searches.AppendInteger(session.GetHabbo().NavigatorLogs.Count);

            foreach (UserSearchLog navi in session.GetHabbo().NavigatorLogs.Values)
            {
                searches.AppendInteger(navi.Id);
                searches.AppendString(navi.Value1);
                searches.AppendString(navi.Value2);
                searches.AppendString(string.Empty);
            }

            session.SendMessage(searches);

            SimpleServerMessageBuffer packetName = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NewNavigatorSizeMessageComposer"));
            UserPreferences pref = session.GetHabbo().Preferences;

            packetName.AppendInteger(pref.NewnaviX);
            packetName.AppendInteger(pref.NewnaviY);
            packetName.AppendInteger(pref.NewnaviWidth);
            packetName.AppendInteger(pref.NewnaviHeight);
            packetName.AppendBool(false);
            packetName.AppendInteger(1);

            session.SendMessage(packetName);
        }
       
        /// <summary>
        ///     Gets the name of the flat cat identifier by.
        /// </summary>
        /// <param name="flatName">Name of the flat category.</param>
        internal int GetFlatCatIdByName(string flatName) => PrivateCategories.Values.Cast<PublicCategory>().First(flat => flat?.Caption == flatName).Id;

        /// <summary>
        ///     Gets a navigator category by caption
        /// </summary>
        /// <param name="navigatorCategoryCaption">Name of the category.</param>
        internal NavigatorCategory GetNavigatorCategory(string navigatorCategoryCaption) => InCategories.FirstOrDefault(c => c.Key == navigatorCategoryCaption).Value;

        /// <summary>
        ///     Gets a Public Room Data
        /// </summary>
        /// <param name="roomId">Public Room Id.</param>
        internal PublicItem GetPublicItem(uint roomId)
        {
            IEnumerable<KeyValuePair<uint, PublicItem>> search = PublicItems.Where(i => i.Value.RoomId == roomId);

            IEnumerable<KeyValuePair<uint, PublicItem>> keyValuePairs = search as KeyValuePair<uint, PublicItem>[] ?? search.ToArray();

            return !keyValuePairs.Any() || keyValuePairs.FirstOrDefault().Value == null ? null : keyValuePairs.FirstOrDefault().Value;
        }

        /// <summary>
        ///     Gets the new length of the navigator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        private static int GetNewNavigatorLength(string value)
        {
            switch (value)
            {
                case "official_view":
                    return 2;

                case "myworld_view":
                    return 5;

                case "hotel_view":
                case "roomads_view":
                    return Yupi.GetGame().GetNavigator().FlatCatsCount + 1;
            }

            return 1;
        }
    }
}