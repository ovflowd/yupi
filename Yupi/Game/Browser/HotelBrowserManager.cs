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
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Browser.Enums;
using Yupi.Game.Browser.Models;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms.Data;
using Yupi.Game.Users;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Browser
{
    /// <summary>
    ///     Class NavigatorManager.
    /// </summary>
    internal class HotelBrowserManager
    {
        /// <summary>
        ///     The _public items
        /// </summary>
        private readonly Dictionary<uint, PublicItem> _publicItems;

        /// <summary>
        ///     The _navigator headers
        /// </summary>
        public readonly List<NavigatorHeader> NavigatorHeaders;

        /// <summary>
        ///     The in categories
        /// </summary>
        internal Dictionary<int, string> InCategories;

        /// <summary>
        ///     The new public rooms
        /// </summary>
        internal ServerMessage NewPublicRooms, NewStaffPicks;

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
            InCategories = new Dictionary<int, string>();
            _publicItems = new Dictionary<uint, PublicItem>();
            NavigatorHeaders = new List<NavigatorHeader>();
            PromoCategories = new Dictionary<int, PromoCategory>();
        }

        /// <summary>
        ///     Gets the flat cats count.
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
            DataTable table = dbClient.GetTable();

            dbClient.SetQuery("SELECT * FROM navigator_publics");
            DataTable table2 = dbClient.GetTable();

            dbClient.SetQuery("SELECT * FROM navigator_pubcats");
            DataTable table3 = dbClient.GetTable();

            dbClient.SetQuery("SELECT * FROM navigator_promocats");
            DataTable table4 = dbClient.GetTable();

            if (table4 != null)
            {
                PromoCategories.Clear();

                foreach (DataRow dataRow in table4.Rows)
                    PromoCategories.Add((int) dataRow["id"],
                        new PromoCategory((int) dataRow["id"], (string) dataRow["caption"], (int) dataRow["min_rank"],
                            Yupi.EnumToBool((string) dataRow["visible"])));
            }

            if (table != null)
            {
                PrivateCategories.Clear();

                foreach (DataRow dataRow in table.Rows)
                    PrivateCategories.Add((int) dataRow["id"],
                        new PublicCategory((int) dataRow["id"], (string) dataRow["caption"], (int) dataRow["min_rank"]));
            }

            if (table2 != null)
            {
                _publicItems.Clear();

                foreach (DataRow row in table2.Rows)
                {
                    _publicItems.Add(Convert.ToUInt32(row["id"]),
                        new PublicItem(Convert.ToUInt32(row["id"]), int.Parse(row["bannertype"].ToString()),
                            (string) row["caption"],
                            (string) row["description"], (string) row["image"],
                            row["image_type"].ToString().ToLower() == "internal"
                                ? PublicImageType.Internal
                                : PublicImageType.External, (uint) row["room_id"], 0, (int) row["category_parent_id"],
                            row["recommended"].ToString() == "1", (int) row["typeofdata"], string.Empty));
                }
            }

            if (table3 != null)
            {
                InCategories.Clear();

                foreach (DataRow dataRow in table3.Rows)
                    InCategories.Add((int) dataRow["id"], (string) dataRow["caption"]);
            }
        }

        public void AddPublicItem(PublicItem item)
        {
            if (item == null)
                return;

            _publicItems.Add(Convert.ToUInt32(item.Id), item);
        }

        public void RemovePublicItem(uint id)
        {
            if (!_publicItems.ContainsKey(id))
                return;

            _publicItems.Remove(id);
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
        public void SerializeNavigatorPopularRoomsNews(ref ServerMessage reply, KeyValuePair<RoomData, uint>[] rooms,
            int category, bool direct)
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
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializePromotionCategories()
        {
            ServerMessage categories =
                new ServerMessage(LibraryParser.OutgoingRequest("CatalogPromotionGetCategoriesMessageComposer"));

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
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeNewPublicRooms()
        {
            ServerMessage message = new ServerMessage();

            message.StartArray();

            foreach (PublicItem item in _publicItems.Values)
            {
                if (item.ParentId == -1)
                {
                    message.Clear();

                    if (item.RoomData == null)
                        continue;

                    item.RoomData.Serialize(message);

                    message.SaveArray();
                }
            }

            message.EndArray();

            return message;
        }

        internal ServerMessage SerializeNewStaffPicks()
        {
            ServerMessage message = new ServerMessage();

            message.StartArray();

            foreach (PublicItem item in _publicItems.Values.Where(t => t.ParentId == -2))
            {
                message.Clear();

                if (item.RoomData == null)
                    continue;

                item.RoomData.Serialize(message);
                message.SaveArray();
            }

            message.EndArray();

            return message;
        }

        /// <summary>
        ///     Gets the flat cat.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>FlatCat.</returns>
        internal PublicCategory GetFlatCat(int id)
            => PrivateCategories.Contains(id) ? (PublicCategory) PrivateCategories[id] : null;

        /// <summary>
        ///     Serializes the nv recommend rooms.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeNvRecommendRooms()
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorLiftedRoomsComposer"));

            message.AppendInteger(_publicItems.Count); //count

            foreach (PublicItem item in _publicItems.Values)
                item.SerializeNew(message);

            return message;
        }

        /// <summary>
        ///     Serializes the new flat categories.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeNewFlatCategories()
        {
            List<PublicCategory> flatcat = Yupi.GetGame().GetNavigator().PrivateCategories.OfType<PublicCategory>().ToList();

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorNewFlatCategoriesMessageComposer"));

            message.AppendInteger(flatcat.Count);

            foreach (PublicCategory cat in flatcat)
            {
                message.AppendInteger(cat.Id);
                message.AppendInteger(cat.UsersNow);
                message.AppendInteger(500);
            }

            return message;
        }

        /// <summary>
        ///     Serializes the nv flat categories.
        /// </summary>
        /// <param name="myWorld">if set to <c>true</c> [my world].</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeNvFlatCategories(bool myWorld)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorMetaDataComposer"));
            message.AppendInteger(InCategories.Count);
            message.AppendString("categories");
            message.AppendInteger(1);

            if (myWorld)
            {
                message.AppendInteger(1);
                message.AppendString("myworld_view");
                message.AppendString("");
                message.AppendString("br");

                foreach (string item in InCategories.Values)
                {
                    message.AppendString(item);
                    message.AppendInteger(1);
                }
            }
            else
            {
                foreach (string item in InCategories.Values)
                {
                    message.AppendString(item);
                    message.AppendInteger(0);
                }
            }

            return message;
        }

        /// <summary>
        ///     Serlializes the new navigator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="session">The session.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeNewNavigator(string value1, string value2, GameClient session)
        {
            ServerMessage newNavigator = new ServerMessage(LibraryParser.OutgoingRequest("SearchResultSetComposer"));

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
        ///     Enables the new navigator.
        /// </summary>
        /// <param name="session">The session.</param>
        internal void EnableNewNavigator(GameClient session)
        {
            ServerMessage navigatorMetaDataParser = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorMetaDataComposer"));

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

            ServerMessage navigatorLiftedRoomsParser =
                new ServerMessage(LibraryParser.OutgoingRequest("NavigatorLiftedRoomsComposer"));
            navigatorLiftedRoomsParser.AppendInteger(NavigatorHeaders.Count);

            foreach (NavigatorHeader navHeader in NavigatorHeaders)
            {
                navigatorLiftedRoomsParser.AppendInteger(navHeader.RoomId);
                navigatorLiftedRoomsParser.AppendInteger(0);
                navigatorLiftedRoomsParser.AppendString(navHeader.Image);
                navigatorLiftedRoomsParser.AppendString(navHeader.Caption);
            }

            session.SendMessage(navigatorLiftedRoomsParser);

            ServerMessage collapsedCategoriesMessageParser = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorCategorys"));

            collapsedCategoriesMessageParser.AppendInteger(FlatCatsCount + 4);

            foreach (PublicCategory flat in PrivateCategories.Values)
                collapsedCategoriesMessageParser.AppendString($"category__{flat.Caption}");

            collapsedCategoriesMessageParser.AppendString("recommended");
            collapsedCategoriesMessageParser.AppendString("new_ads");
            collapsedCategoriesMessageParser.AppendString("staffpicks");
            collapsedCategoriesMessageParser.AppendString("official");
            session.SendMessage(collapsedCategoriesMessageParser);

            ServerMessage searches = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorSavedSearchesComposer"));

            searches.AppendInteger(session.GetHabbo().NavigatorLogs.Count);

            foreach (UserSearchLog navi in session.GetHabbo().NavigatorLogs.Values)
            {
                searches.AppendInteger(navi.Id);
                searches.AppendString(navi.Value1);
                searches.AppendString(navi.Value2);
                searches.AppendString("");
            }

            session.SendMessage(searches);

            ServerMessage packetName = new ServerMessage(LibraryParser.OutgoingRequest("NewNavigatorSizeMessageComposer"));
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
        ///     Serializes the flat categories.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeFlatCategories(GameClient session)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("FlatCategoriesMessageComposer"));
            serverMessage.StartArray();

            foreach (PublicCategory flatCat in PrivateCategories.Values)
            {
                serverMessage.Clear();

                if (flatCat == null)
                    continue;

                serverMessage.AppendInteger(flatCat.Id);
                serverMessage.AppendString(flatCat.Caption);
                serverMessage.AppendBool(flatCat.MinRank <= session.GetHabbo().Rank);
                serverMessage.AppendBool(false);
                serverMessage.AppendString("NONE");
                serverMessage.AppendString(string.Empty);
                serverMessage.AppendBool(false);

                serverMessage.SaveArray();
            }

            serverMessage.EndArray();

            return serverMessage;
        }

        /// <summary>
        ///     Gets the name of the flat cat identifier by.
        /// </summary>
        /// <param name="flatName">Name of the flat.</param>
        /// <returns>System.Int32.</returns>
        internal int GetFlatCatIdByName(string flatName)
        {
            foreach (PublicCategory flat in PrivateCategories.Values.Cast<PublicCategory>().Where(flat => flat.Caption == flatName)
                )
                return flat.Id;

            return -1;
        }

        /// <summary>
        ///     Serializes the public rooms.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializePublicRooms()
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("OfficialRoomsMessageComposer"));
            PublicItem[] rooms =
                _publicItems.Values.Where(current => current.ParentId <= 0 && current.RoomData != null).ToArray();

            serverMessage.AppendInteger(rooms.Length);

            foreach (PublicItem current in rooms)
            {
                current.Serialize(serverMessage);

                if (current.ItemType != PublicItemType.Category)
                    continue;

                foreach (PublicItem current2 in _publicItems.Values.Where(x => x.ParentId == current.Id))
                    current2.Serialize(serverMessage);
            }

            if (!_publicItems.Values.Any(current => current.Recommended))
                serverMessage.AppendInteger(0);
            else
            {
                PublicItem room = _publicItems.Values.First(current => current.Recommended);

                if (room != null)
                {
                    serverMessage.AppendInteger(1);
                    room.Serialize(serverMessage);
                }
                else
                    serverMessage.AppendInteger(0);
            }
            serverMessage.AppendInteger(0);

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the favorite rooms.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeFavoriteRooms(GameClient session)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorListingsMessageComposer"));
            serverMessage.AppendInteger(6);
            serverMessage.AppendString(string.Empty);
            serverMessage.AppendInteger(session.GetHabbo().FavoriteRooms.Count);

            uint[] array = session.GetHabbo().FavoriteRooms.ToArray();

            foreach (
                RoomData roomData in
                    array.Select(roomId => Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId))
                        .Where(roomData => roomData != null))
                roomData.Serialize(serverMessage);

            serverMessage.AppendBool(false);

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the recent rooms.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeRecentRooms(GameClient session)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorListingsMessageComposer"));
            serverMessage.AppendInteger(7);
            serverMessage.AppendString(string.Empty);

            serverMessage.StartArray();

            foreach (
                RoomData roomData in
                    session.GetHabbo()
                        .RecentlyVisitedRooms.Select(
                            current => Yupi.GetGame().GetRoomManager().GenerateRoomData(current)))
            {
                roomData.Serialize(serverMessage);
                serverMessage.SaveArray();
            }

            serverMessage.EndArray();
            serverMessage.AppendBool(false);

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the event listing.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeEventListing()
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorListingsMessageComposer"));
            serverMessage.AppendInteger(16);
            serverMessage.AppendString(string.Empty);

            KeyValuePair<RoomData, uint>[] eventRooms = Yupi.GetGame().GetRoomManager().GetEventRooms();

            serverMessage.AppendInteger(eventRooms.Length);

            KeyValuePair<RoomData, uint>[] array = eventRooms;

            foreach (KeyValuePair<RoomData, uint> keyValuePair in array)
                keyValuePair.Key.Serialize(serverMessage, true);

            return serverMessage;
        }

        internal PublicItem GetPublicItem(uint roomId)
        {
            IEnumerable<KeyValuePair<uint, PublicItem>> search = _publicItems.Where(i => i.Value.RoomId == roomId);

            IEnumerable<KeyValuePair<uint, PublicItem>> keyValuePairs = search as KeyValuePair<uint, PublicItem>[] ??
                                                                        search.ToArray();

            return !keyValuePairs.Any() || keyValuePairs.FirstOrDefault().Value == null
                ? null
                : keyValuePairs.FirstOrDefault().Value;
        }

        /// <summary>
        ///     Serializes the popular room tags.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializePopularRoomTags()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            ServerMessage result;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT tags, users_now FROM rooms_data WHERE roomtype = 'private' AND users_now > 0 ORDER BY users_now DESC LIMIT 50");

                DataTable table = commitableQueryReactor.GetTable();

                if (table != null)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        int usersNow;

                        if (!string.IsNullOrEmpty(dataRow["users_now"].ToString()))
                            usersNow = (int) dataRow["users_now"];
                        else
                            usersNow = 0;

                        string[] array = dataRow["tags"].ToString().Split(',');
                        List<string> list = array.ToList();

                        foreach (string current in list)
                        {
                            if (dictionary.ContainsKey(current))
                                dictionary[current] += usersNow;
                            else
                                dictionary.Add(current, usersNow);
                        }
                    }
                }

                List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>(dictionary);

                list2.Sort((firstPair, nextPair) => firstPair.Value.CompareTo(nextPair.Value));

                ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PopularRoomTagsMessageComposer"));
                serverMessage.AppendInteger(list2.Count);

                foreach (KeyValuePair<string, int> current2 in list2)
                {
                    serverMessage.AppendString(current2.Key);
                    serverMessage.AppendInteger(current2.Value);
                }

                result = serverMessage;
            }

            return result;
        }

        /// <summary>
        ///     Serializes the navigator.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeNavigator(GameClient session, int mode)
        {
            if (mode >= 0)
                return SerializeActiveRooms(mode);

            ServerMessage reply = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorListingsMessageComposer"));

            switch (mode)
            {
                case -6:
                {
                    reply.AppendInteger(14);

                    List<RoomData> activeGRooms = new List<RoomData>();

                    KeyValuePair<RoomData, uint>[] rooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();

                    if (rooms != null && rooms.Any())
                    {
                        activeGRooms.AddRange(from rd in rooms where rd.Key.GroupId != 0 select rd.Key);
                        activeGRooms = activeGRooms.OrderByDescending(p => p.UsersNow).ToList();
                    }

                    SerializeNavigatorRooms(ref reply, activeGRooms);

                    return reply;
                }
                case -5:
                case -4:
                {
                    reply.AppendInteger(mode*-1);

                    List<RoomData> activeFriends =
                        session.GetHabbo()
                            .GetMessenger()
                            .GetActiveFriendsRooms()
                            .OrderByDescending(p => p.UsersNow)
                            .ToList();
                    SerializeNavigatorRooms(ref reply, activeFriends);

                    return reply;
                }
                case -3:
                {
                    reply.AppendInteger(5);
                    SerializeNavigatorRooms(ref reply, session.GetHabbo().UsersRooms);

                    return reply;
                }
                case -2:
                {
                    reply.AppendInteger(2);

                    try
                    {
                        KeyValuePair<RoomData, int>[] rooms = Yupi.GetGame().GetRoomManager().GetVotedRooms();

                        SerializeNavigatorRooms(ref reply, rooms);

                        if (rooms != null)
                            Array.Clear(rooms, 0, rooms.Length);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        reply.AppendString(string.Empty);
                        reply.AppendInteger(0);
                    }

                    return reply;
                }
                case -1:
                {
                    reply.AppendInteger(1);
                    reply.AppendString("-1");

                    try
                    {
                        KeyValuePair<RoomData, uint>[] rooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();

                        SerializeNavigatorPopularRooms(ref reply, rooms);

                        if (rooms != null)
                            Array.Clear(rooms, 0, rooms.Length);
                    }
                    catch
                    {
                        reply.AppendInteger(0);
                        reply.AppendBool(false);
                    }

                    return reply;
                }
            }

            return reply;
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

        /// <summary>
        ///     Serializes the active rooms.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>ServerMessage.</returns>
        private static ServerMessage SerializeActiveRooms(int category) => null;

        /// <summary>
        ///     Serializes the navigator rooms.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="rooms">The rooms.</param>
        private static void SerializeNavigatorRooms(ref ServerMessage reply, ICollection<RoomData> rooms)
        {
            reply.AppendString(string.Empty);

            if (!rooms?.Any() ?? true)
            {
                reply.AppendInteger(0);
                reply.AppendBool(false);

                return;
            }

            if (rooms != null)
            {
                reply.AppendInteger(rooms.Count);

                foreach (RoomData pair in rooms)
                    pair.Serialize(reply);
            }

            reply.AppendBool(false);
        }

        /// <summary>
        ///     Serializes the navigator popular rooms.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="rooms">The rooms.</param>
        private static void SerializeNavigatorPopularRooms(ref ServerMessage reply,
            ICollection<KeyValuePair<RoomData, uint>> rooms)
        {
            if (!rooms?.Any() ?? true)
            {
                reply.AppendInteger(0);
                reply.AppendBool(false);
                return;
            }

            if (rooms != null)
            {
                reply.AppendInteger(rooms.Count);

                foreach (KeyValuePair<RoomData, uint> pair in rooms)
                    pair.Key.Serialize(reply);
            }

            reply.AppendBool(false);
        }

        /// <summary>
        ///     Serializes the navigator rooms.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="rooms">The rooms.</param>
        private static void SerializeNavigatorRooms(ref ServerMessage reply,
            ICollection<KeyValuePair<RoomData, int>> rooms)
        {
            reply.AppendString(string.Empty);

            if (!rooms?.Any() ?? true)
            {
                reply.AppendInteger(0);
                reply.AppendBool(false);

                return;
            }

            if (rooms != null)
            {
                reply.AppendInteger(rooms.Count);

                foreach (KeyValuePair<RoomData, int> pair in rooms)
                    pair.Key.Serialize(reply);
            }

            reply.AppendBool(false);
        }

        /// <summary>
        ///     Serializes the promoted.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>ServerMessage.</returns>
        public static ServerMessage SerializePromoted(GameClient session, int mode)
        {
            ServerMessage reply = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorListingsMessageComposer"));

            reply.AppendInteger(mode);
            reply.AppendString(string.Empty);

            try
            {
                KeyValuePair<RoomData, uint>[] rooms = Yupi.GetGame().GetRoomManager().GetEventRooms();

                SerializeNavigatorPopularRooms(ref reply, rooms);

                if (rooms != null)
                    Array.Clear(rooms, 0, rooms.Length);
            }
            catch
            {
                reply.AppendInteger(0);
                reply.AppendBool(false);
            }

            return reply;
        }

        /// <summary>
        ///     Serializes the search results.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <returns>ServerMessage.</returns>
        public static ServerMessage SerializeSearchResults(string searchQuery)
        {
            bool containsOwner = false;
            bool containsGroup = false;
            string originalQuery = searchQuery;

            if (searchQuery.StartsWith("owner:"))
            {
                searchQuery = searchQuery.Replace("owner:", string.Empty);
                containsOwner = true;
            }
            else if (searchQuery.StartsWith("group:"))
            {
                searchQuery = searchQuery.Replace("group:", string.Empty);
                containsGroup = true;
            }

            List<RoomData> rooms = new List<RoomData>();

            if (!containsOwner)
            {
                bool initForeach = false;

                KeyValuePair<RoomData, uint>[] activeRooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();
                try
                {
                    if (activeRooms != null && activeRooms.Any())
                        initForeach = true;
                }
                catch
                {
                    initForeach = false;
                }

                if (initForeach)
                {
                    foreach (KeyValuePair<RoomData, uint> rms in activeRooms)
                    {
                        if (rms.Key.Name.ToLower().Contains(searchQuery.ToLower()) && rooms.Count <= 50)
                            rooms.Add(rms.Key);
                        else
                            break;
                    }
                }
            }

            if (rooms.Count < 50 || containsOwner || containsGroup)
            {
                DataTable dTable;

                using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    if (containsOwner)
                    {
                        dbClient.SetQuery(
                            "SELECT rooms_data.* FROM rooms_data LEFT OUTER JOIN users ON rooms_data.owner = users.id WHERE users.username = @query AND rooms_data.roomtype = 'private' LIMIT 50");
                        dbClient.AddParameter("query", searchQuery);
                        dTable = dbClient.GetTable();
                    }
                    else if (containsGroup)
                    {
                        dbClient.SetQuery(
                            "SELECT * FROM rooms_data JOIN groups_data ON rooms_data.id = groups_data.room_id WHERE groups_data.group_name LIKE @query AND roomtype = 'private' LIMIT 50");
                        dbClient.AddParameter("query", "%" + searchQuery + "%");
                        dTable = dbClient.GetTable();
                    }
                    else
                    {
                        dbClient.SetQuery(
                            "SELECT * FROM rooms_data WHERE caption = @query AND roomtype = 'private' LIMIT " +
                            (50 - rooms.Count));
                        dbClient.AddParameter("query", searchQuery);
                        dTable = dbClient.GetTable();
                    }
                }

                if (dTable != null)
                {
                    foreach (RoomData rData in dTable.Rows.Cast<DataRow>().Select(row => Yupi.GetGame()
                        .GetRoomManager()
                        .FetchRoomData(Convert.ToUInt32(row["id"]), row)).Where(rData => !rooms.Contains(rData)))
                        rooms.Add(rData);
                }
            }

            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("NavigatorListingsMessageComposer"));
            message.AppendInteger(8);
            message.AppendString(originalQuery);
            message.AppendInteger(rooms.Count);

            foreach (RoomData room in rooms)
                room.Serialize(message);

            message.AppendBool(false);

            return message;
        }
    }
}