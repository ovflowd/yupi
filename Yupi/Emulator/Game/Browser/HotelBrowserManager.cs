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
using Yupi.Emulator.Game.Browser.Composers;
using Yupi.Emulator.Game.Browser.Enums;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Browser
{
    /// <summary>
    ///     Class NavigatorManager.
    /// </summary>
     public class HotelBrowserManager
    {
        /// <summary>
        ///     The _public items
        /// </summary>
        public readonly Dictionary<uint, PublicItem> PublicRooms;

        /// <summary>
        ///     The _navigator headers
        /// </summary>
        public readonly List<NavigatorHeader> NavigatorHeaders;

        /// <summary>
        ///     The in categories
        /// </summary>
     public Dictionary<string, NavigatorCategory> NavigatorCategories;

        /// <summary>
        ///     The private categories
        /// </summary>
     public HybridDictionary PrivateCategories;

        /// <summary>
        ///     The promo categories
        /// </summary>
     public Dictionary<int, PromoCategory> PromoCategories;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelBrowserManager" /> class.
        /// </summary>
     public HotelBrowserManager()
        {
            PrivateCategories = new HybridDictionary();

            NavigatorCategories = new Dictionary<string, NavigatorCategory>();

            PublicRooms = new Dictionary<uint, PublicItem>();

            NavigatorHeaders = new List<NavigatorHeader>();

            PromoCategories = new Dictionary<int, PromoCategory>();
        }

        /// <summary>
        ///     Get the Number of Flat Caegories
        /// </summary>
        /// <value>The flat cats count.</value>
     public int FlatCatsCount => PrivateCategories.Count;

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
            dbClient.SetQuery("SELECT * FROM navigator_flatcats WHERE `enabled` = '2'");
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
                PublicRooms.Clear();

                foreach (DataRow row in navigatorPublicRooms.Rows)
                    PublicRooms.Add(Convert.ToUInt32(row["id"]),
                        new PublicItem(Convert.ToUInt32(row["id"]), int.Parse(row["bannertype"].ToString()),
                            (string) row["caption"],
                            (string) row["description"], (string) row["image"],
                            row["image_type"].ToString().ToLower() == ""
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
                    NavigatorCategories.Clear();

                    foreach (DataRow dataRow in navigatorPublicCats.Rows)
                        NavigatorCategories.Add((string)dataRow["caption"], new NavigatorCategory((int)dataRow["id"], (string)dataRow["caption"], (string)dataRow["default_state"] == "opened", (string)dataRow["default_size"] == "image", subCategories.Where(c => c.MainCategory == (string)dataRow["caption"]).ToList()));
                }
            }
        }

        public void AddPublicRoom(PublicItem item)
        {
            if (item == null)
                return;

            PublicRooms.Add(Convert.ToUInt32(item.Id), item);
        }

        public void RemovePublicRoom(uint id)
        {
            if (!PublicRooms.ContainsKey(id))
                return;

            PublicRooms.Remove(id);
        }

        /// <summary>
        ///     Serializes the navigator popular rooms news.
        /// </summary>
        /// <param name="reply">The reply.</param>
        /// <param name="rooms">The rooms.</param>
        /// <param name="category">The category.</param>
        /// <param name="direct">if set to <c>true</c> [direct].</param>
        public void SerializeNavigatorPromotedRooms(ref SimpleServerMessageBuffer reply, KeyValuePair<RoomData, uint>[] rooms, int category, bool direct)
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
        ///     Serializes the new public rooms.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer SerializePublicRooms()
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();

            messageBuffer.StartArray();

            foreach (PublicItem item in PublicRooms.Values)
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

     public SimpleServerMessageBuffer SerializeStaffPicks()
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();

            messageBuffer.StartArray();

            foreach (PublicItem item in PublicRooms.Values.Where(t => t.ParentId == -2))
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
     public PublicCategory GetFlatCat(int id)
            => PrivateCategories.Contains(id) ? (PublicCategory) PrivateCategories[id] : null;

        /// <summary>
        ///     Initializes Navigator
        /// </summary>
        /// <param name="session">The session.</param>
     public void InitializeNavigator(GameClient session)
        {
            session.SendMessage(NavigatorMetaDataComposer.Compose());

            session.SendMessage(NavigatorLiftedRoomsComposer.Compose());
            
            session.SendMessage(NavigatorCategoriesListComposer.Compose());

            session.SendMessage(NavigatorSavedSearchesComposer.Compose(session));

            session.SendMessage(NavigatorPreferencesComposer.Compose(session));
        }
       
        /// <summary>
        ///     Gets the name of the flat cat identifier by.
        /// </summary>
        /// <param name="flatName">Name of the flat category.</param>
     public int GetFlatCatIdByName(string flatName) => PrivateCategories.Values.Cast<PublicCategory>().First(flat => flat?.Caption == flatName).Id;

        /// <summary>
        ///     Gets a navigator category by caption
        /// </summary>
        /// <param name="navigatorCategoryCaption">Name of the category.</param>
     public NavigatorCategory GetNavigatorCategory(string navigatorCategoryCaption) => NavigatorCategories.FirstOrDefault(c => c.Key == navigatorCategoryCaption).Value;

        /// <summary>
        ///     Gets a Public Room Data
        /// </summary>
        /// <param name="roomId">Public Room Id.</param>
     public PublicItem GetPublicRoom(uint roomId)
        {
            IEnumerable<KeyValuePair<uint, PublicItem>> search = PublicRooms.Where(i => i.Value.RoomId == roomId);

            IEnumerable<KeyValuePair<uint, PublicItem>> keyValuePairs = search as KeyValuePair<uint, PublicItem>[] ?? search.ToArray();

            return !keyValuePairs.Any() || keyValuePairs.FirstOrDefault().Value == null ? null : keyValuePairs.FirstOrDefault().Value;
        }

        /// <summary>
        ///     Gets the new length of the navigator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
     public int GetNewNavigatorLength(string value)
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