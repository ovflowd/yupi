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
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Browser.Models;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Groups.Structs;
using Yupi.Game.Rooms.Data;
using Yupi.Messages;

namespace Yupi.Game.Browser
{
    /// <summary>
    ///     Class SearchResultList.
    /// </summary>
    internal class SearchResultList
    {
        /// <summary>
        ///     Serializes the search result list flatcats.
        /// </summary>
        /// <param name="flatCatId">The flat cat identifier.</param>
        /// <param name="direct">if set to <c>true</c> [direct].</param>
        /// <param name="message">The message.</param>
        internal static void SerializeSearchResultListFlatcats(int flatCatId, bool direct, ServerMessage message)
        {
            PublicCategory flatCat = Yupi.GetGame().GetNavigator().GetFlatCat(flatCatId);

            if (flatCat == null)
                return;

            message.AppendString($"category__{flatCat.Caption}");
            message.AppendString(flatCat.Caption);
            message.AppendInteger(0);
            message.AppendBool(true);
            message.AppendInteger(-1);

            KeyValuePair<RoomData, uint>[] rooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();
            Yupi.GetGame().GetNavigator().SerializeNavigatorPopularRoomsNews(ref message, rooms, flatCatId, direct);
        }

        /// <summary>
        ///     Serializes the promotions result list flatcats.
        /// </summary>
        /// <param name="flatCatId">The flat cat identifier.</param>
        /// <param name="direct">if set to <c>true</c> [direct].</param>
        /// <param name="message">The message.</param>
        internal static void SerializePromotionsResultListFlatcats(int flatCatId, bool direct, ServerMessage message)
        {
            PublicCategory flatCat = Yupi.GetGame().GetNavigator().GetFlatCat(flatCatId);

            message.AppendString("new_ads");
            message.AppendString(flatCat.Caption);
            message.AppendInteger(0);
            message.AppendBool(true);
            message.AppendInteger(-1);

            KeyValuePair<RoomData, uint>[] rooms = Yupi.GetGame().GetRoomManager().GetEventRooms();
            Yupi.GetGame().GetNavigator().SerializeNavigatorPopularRoomsNews(ref message, rooms, flatCatId, direct);
        }

        /// <summary>
        ///     Serializes the search result list statics.
        /// </summary>
        /// <param name="staticId">The static identifier.</param>
        /// <param name="direct">if set to <c>true</c> [direct].</param>
        /// <param name="message">The message.</param>
        /// <param name="session">The session.</param>
        internal static void SerializeSearchResultListStatics(string staticId, bool direct, ServerMessage message,
            GameClient session)
        {
            if (string.IsNullOrEmpty(staticId) || staticId == "official")
                staticId = "official_view";

            if (staticId != "hotel_view" && staticId != "roomads_view" && staticId != "myworld_view" &&
                !staticId.StartsWith("category__") && staticId != "official_view")
            {
                message.AppendString(staticId);
                message.AppendString(string.Empty);
                message.AppendInteger(1);
                message.AppendBool(staticId != "my" && staticId != "popular" && staticId != "official-root");
                message.AppendInteger(staticId == "official-root" ? 1 : 0);
            }

            KeyValuePair<RoomData, uint>[] rooms;

            switch (staticId)
            {
                case "hotel_view":
                {
                    SerializeSearchResultListStatics("popular", false, message, session);

                    foreach (PublicCategory flat in Yupi.GetGame().GetNavigator().PrivateCategories.Values)
                        SerializeSearchResultListFlatcats(flat.Id, false, message);

                    break;
                }
                case "myworld_view":
                {
                    SerializeSearchResultListStatics("my", false, message, session);
                    SerializeSearchResultListStatics("favorites", false, message, session);
                    SerializeSearchResultListStatics("my_groups", false, message, session);
                    SerializeSearchResultListStatics("history", false, message, session);
                    SerializeSearchResultListStatics("friends_rooms", false, message, session);

                    break;
                }
                case "roomads_view":
                {
                    foreach (PublicCategory flat in Yupi.GetGame().GetNavigator().PrivateCategories.Values)
                        SerializePromotionsResultListFlatcats(flat.Id, false, message);

                    SerializeSearchResultListStatics("top_promotions", false, message, session);

                    break;
                }
                case "official_view":
                {
                    SerializeSearchResultListStatics("official-root", false, message, session);
                    SerializeSearchResultListStatics("staffpicks", false, message, session);

                    break;
                }
                case "official-root":
                {
                    message.AppendServerMessage(Yupi.GetGame().GetNavigator().NewPublicRooms);

                    break;
                }
                case "staffpicks":
                {
                    message.AppendServerMessage(Yupi.GetGame().GetNavigator().NewStaffPicks);

                    break;
                }
                case "my":
                {
                    int i = 0;

                    message.StartArray();

                    foreach (RoomData data in session.GetHabbo().UsersRooms)
                    {
                        if (data != null)
                        {
                            data.Serialize(message);
                            message.SaveArray();

                            if (i++ == (direct ? 100 : 8))
                                break;
                        }
                    }

                    message.EndArray();

                    break;
                }
                case "favorites":
                {
                    if (session.GetHabbo().FavoriteRooms == null)
                    {
                        message.AppendInteger(0);

                        return;
                    }

                    int i = 0;

                    message.AppendInteger(session.GetHabbo().FavoriteRooms.Count > (direct ? 40 : 8)
                        ? (direct ? 40 : 8)
                        : session.GetHabbo().FavoriteRooms.Count);

                    foreach (
                        RoomData data in
                            session.GetHabbo()
                                .FavoriteRooms.Select(dataId => Yupi.GetGame().GetRoomManager().GenerateRoomData(dataId))
                                .Where(data => data != null))
                    {
                        data.Serialize(message);

                        i++;

                        if (i == (direct ? 40 : 8))
                            break;
                    }

                    break;
                }
                case "friends_rooms":
                {
                    int i = 0;

                    if (session?.GetHabbo() == null || session.GetHabbo().GetMessenger() == null ||
                        session.GetHabbo().GetMessenger().GetActiveFriendsRooms() == null)
                    {
                        message.AppendInteger(0);

                        return;
                    }

                    List<RoomData> roomsFriends =
                        session.GetHabbo()
                            .GetMessenger()
                            .GetActiveFriendsRooms()
                            .OrderByDescending(p => p.UsersNow)
                            .Take(direct ? 40 : 8)
                            .ToList();

                    message.AppendInteger(roomsFriends.Count);

                    foreach (RoomData data in roomsFriends.Where(data => data != null))
                    {
                        data.Serialize(message);

                        i++;

                        if (i == (direct ? 40 : 8))
                            break;
                    }

                    break;
                }
                case "recommended":
                {
                    break;
                }
                case "popular":
                {
                    rooms = Yupi.GetGame().GetRoomManager().GetActiveRooms();

                    if (rooms == null)
                    {
                        message.AppendInteger(0);

                        break;
                    }

                    message.AppendInteger(rooms.Length);

                    foreach (KeyValuePair<RoomData, uint> room in rooms)
                        room.Key.Serialize(message);

                    break;
                }
                case "top_promotions":
                {
                    rooms = Yupi.GetGame().GetRoomManager().GetEventRooms();

                    message.AppendInteger(rooms.Length);

                    foreach (KeyValuePair<RoomData, uint> room in rooms)
                        room.Key.Serialize(message);

                    break;
                }
                case "my_groups":
                {
                    int i = 0;

                    message.StartArray();

                    foreach (uint xGroupId in session.GetHabbo().MyGroups)
                    {
                        Group xGroup = Yupi.GetGame().GetGroupManager().GetGroup(xGroupId);

                        if (xGroup != null)
                        {
                            RoomData data = Yupi.GetGame().GetRoomManager().GenerateRoomData(xGroup.RoomId);

                            if (data != null)
                            {
                                data.Serialize(message);
                                message.SaveArray();

                                if (i++ == (direct ? 40 : 8))
                                    break;
                            }
                        }
                    }

                    message.EndArray();

                    break;
                }
                case "history":
                {
                    int i = 0;

                    message.StartArray();

                    foreach (
                        RoomData roomData in
                            session.GetHabbo()
                                .RecentlyVisitedRooms.Select(
                                    roomId => Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId))
                                .Where(roomData => roomData != null))
                    {
                        roomData.Serialize(message);
                        message.SaveArray();

                        if (i++ == (direct ? 40 : 8))
                            break;
                    }

                    message.EndArray();

                    break;
                }
                default:
                {
                    if (staticId.StartsWith("category__"))
                        SerializeSearchResultListFlatcats(
                            Yupi.GetGame()
                                .GetNavigator()
                                .GetFlatCatIdByName(staticId.Replace("category__", string.Empty)), true, message);
                    else
                        message.AppendInteger(0);

                    break;
                }
            }
        }

        /// <summary>
        ///     Serializes the searches.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="message">The message.</param>
        /// <param name="session">The session.</param>
        internal static void SerializeSearches(string searchQuery, ServerMessage message, GameClient session)
        {
            message.AppendString(string.Empty);

            message.AppendString(searchQuery);
            message.AppendInteger(2);
            message.AppendBool(false);
            message.AppendInteger(0);

            bool containsOwner = false, containsGroup = false;

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
                bool initForeach = Yupi.GetGame().GetRoomManager().GetActiveRooms() != null &&
                                  Yupi.GetGame().GetRoomManager().GetActiveRooms().Any();

                if (initForeach)
                {
                    foreach (KeyValuePair<RoomData, uint> rms in Yupi.GetGame().GetRoomManager().GetActiveRooms())
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
                            "SELECT rooms_data.* FROM rooms_data LEFT OUTER JOIN users ON rooms_data.owner = users.id WHERE users.username LIKE @query AND rooms_data.roomtype = 'private' LIMIT 50");
                        dbClient.AddParameter("query", searchQuery);
                        dTable = dbClient.GetTable();
                    }
                    else if (containsGroup)
                    {
                        dbClient.SetQuery(
                            "SELECT * FROM rooms_data JOIN groups_data ON rooms_data.id = groups_data.room_id WHERE groups_data.group_name LIKE @query AND roomtype = 'private' LIMIT 50");
                        dbClient.AddParameter("query", $"%{searchQuery}%");
                        dTable = dbClient.GetTable();
                    }
                    else
                    {
                        dbClient.SetQuery(
                            $"SELECT * FROM rooms_data WHERE caption LIKE @query AND roomtype = 'private' LIMIT {50 - rooms.Count}");
                        dbClient.AddParameter("query", searchQuery);
                        dTable = dbClient.GetTable();
                    }
                }

                if (dTable != null)
                {
                    foreach (
                        RoomData rData in
                            dTable.Rows.Cast<DataRow>()
                                .Select(
                                    row =>
                                        Yupi.GetGame().GetRoomManager().FetchRoomData(Convert.ToUInt32(row["id"]), row))
                                .Where(rData => !rooms.Contains(rData)))
                        rooms.Add(rData);
                }
            }

            message.AppendInteger(rooms.Count);

            foreach (RoomData data in rooms.Where(data => data != null))
                data.Serialize(message);
        }
    }
}