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

using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser.Composers;
using Yupi.Emulator.Game.Browser.Enums;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary>
        ///     Gets the pub.
        /// </summary>
        internal void GetPub()
        {
            uint roomId = Request.GetUInteger();
            
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);
            
            if (roomData == null)
                return;
                
            //@TODO: What The Hell? Direct Packet?
            GetResponse().Init(PacketLibraryManager.SendRequest("453"));

            GetResponse().AppendInteger(roomData.Id);
            GetResponse().AppendString(roomData.CcTs);
            GetResponse().AppendInteger(roomData.Id);

            SendResponse();
        }

        /// <summary>
        ///     Opens the pub.
        /// </summary>
        internal void OpenPub()
        {
            Request.GetInteger();

            uint roomId = Request.GetUInteger();
            
            Request.GetInteger();
            
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);
            
            if (roomData == null)
                return;
                
            PrepareRoomForUser(roomData.Id, string.Empty);
        }

        /// <summary>
        ///     News the navigator.
        /// </summary>
        internal void NewNavigator()
        {
            if (Session == null)
                return;
                
            Yupi.GetGame().GetNavigator().InitializeNavigator(Session);
        }

        /// <summary>
        ///     Searches the new navigator.
        /// </summary>
        internal void SearchNewNavigator()
        {
            if (Session == null)
                return;
                
            string name = Request.GetString();

            string junk = Request.GetString();
            
            Session.SendMessage(NavigatorSearchListResultComposer.Compose(name, junk, Session));
        }

        /// <summary>
        ///     Saveds the search.
        /// </summary>
        internal void SavedSearch()
        {
            if (Session.GetHabbo().NavigatorLogs.Count > 50)
            {
                Session.SendNotif(Yupi.GetLanguage().GetVar("navigator_max"));
                
                return;
            }
            
            string value1 = Request.GetString();

            string value2 = Request.GetString();
            
            UserSearchLog naviLogs = new UserSearchLog(Session.GetHabbo().NavigatorLogs.Count, value1, value2);
            
            if (!Session.GetHabbo().NavigatorLogs.ContainsKey(naviLogs.Id))
                Session.GetHabbo().NavigatorLogs.Add(naviLogs.Id, naviLogs);
                
            Session.SendMessage(NavigatorSavedSearchesComposer.Compose(Session.GetHabbo().NavigatorLogs));
        }

        /// <summary>
        ///     News the navigator resize.
        /// </summary>
        internal void NewNavigatorResize()
        {
            int x = Request.GetInteger();
            int y = Request.GetInteger();
            int width = Request.GetInteger();
            int height = Request.GetInteger();
            
            Session.GetHabbo().Preferences.NewnaviX = x;
            Session.GetHabbo().Preferences.NewnaviY = y;
            Session.GetHabbo().Preferences.NavigatorWidth = width;
            Session.GetHabbo().Preferences.NavigatorHeight = height;
            Session.GetHabbo().Preferences.Save();
        }

        /// <summary>
        ///     News the navigator add saved search.
        /// </summary>
        internal void NewNavigatorAddSavedSearch() => SavedSearch();

        /// <summary>
        ///     News the navigator delete saved search.
        /// </summary>
        internal void NewNavigatorDeleteSavedSearch()
        {
            int searchId = Request.GetInteger();
            
            if (!Session.GetHabbo().NavigatorLogs.ContainsKey(searchId))
                return;
                
            Session.GetHabbo().NavigatorLogs.Remove(searchId);
            
            Session.SendMessage(NavigatorSavedSearchesComposer.Compose(Session.GetHabbo().NavigatorLogs));
        }

        /// <summary>
        ///     Gets the room information.
        /// </summary>
        internal void GetRoomInfo()
        {
            if (Session.GetHabbo() == null)
                return;
                
            uint roomId = Request.GetUInteger();
            Request.GetBool();
            Request.GetBool();
            
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);
            
            if (roomData == null)
                return;
            
            // @TODO: What The Hell? Directly Packet ID?
            GetResponse().Init(PacketLibraryManager.SendRequest("1491"));
            
            GetResponse().AppendInteger(0);
            roomData.Serialize(GetResponse());
            
            SendResponse();
        }

        /// <summary>
        ///     News the navigator flat cats.
        /// </summary>
        internal void NewNavigatorFlatCats()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(NavigatorFlatCategoriesListComposer.Compose());
        }

        /// <summary>
        ///     Opens the flat.
        /// </summary>
        internal void OpenFlat()
        {
            if (Session.GetHabbo() == null)
                return;

            uint roomId = Request.GetUInteger();
            
            string pWd = Request.GetString();

            PrepareRoomForUser(roomId, pWd);
        }

        internal void ToggleStaffPick()
        {
            uint roomId = Request.GetUInteger();
            
            Request.GetBool();
            
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);
            
            Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(Session, "ACH_Spr", 1, true);
            
            if (room == null)
                return;
            
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                PublicItem pubItem = Yupi.GetGame().GetNavigator().GetPublicRoom(roomId);
                
                if (pubItem == null) // Isn't A Staff Pick Room
                {
                    queryReactor.SetQuery("INSERT INTO navigator_publics (bannertype, room_id, category_parent_id) VALUES ('0', @roomId, '-2')");
                    
                    queryReactor.AddParameter("roomId", room.RoomId);
                    
                    uint lastInsertId = (uint) queryReactor.InsertQuery();
                    
                    PublicItem publicItem = new PublicItem(lastInsertId, 0, string.Empty, string.Empty, string.Empty, PublicImageType.Internal, room.RoomId, 0, -2, false, 1);
                    
                    Yupi.GetGame().GetNavigator().AddPublicRoom(publicItem);
                }
                else // Is a Staff Pick Room
                {
                    queryReactor.SetQuery("DELETE FROM navigator_publics WHERE id = @pubId");
                    
                    queryReactor.AddParameter("pubId", pubItem.Id);
                    queryReactor.RunQuery();
                    
                    Yupi.GetGame().GetNavigator().RemovePublicRoom(pubItem.Id);
                }
                
                room.RoomData.SerializeRoomData(Response, Session, false, true);
            }
        }
    }
}
