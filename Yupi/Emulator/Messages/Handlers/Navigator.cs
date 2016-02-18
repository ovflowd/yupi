using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Browser;
using Yupi.Emulator.Game.Browser.Enums;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.Data;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Messages.Handlers
{
    /// <summary>
    ///     Class GameClientMessageHandler.
    /// </summary>
    internal partial class GameClientMessageHandler
    {
        /// <summary> 
        ///     Gets the flat cats.
        /// </summary>
        internal void GetFlatCats()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeFlatCategories(Session));
        }

        /// <summary>
        ///     Enters the inquired room.
        /// </summary>
        internal void EnterInquiredRoom()
        {
            
        }

        /// <summary>
        ///     Gets the pub.
        /// </summary>
        internal void GetPub()
        {
            uint roomId = Request.GetUInteger();
            
            RoomData roomData = Yupi.GetGame().GetRoomManager().GenerateRoomData(roomId);
            
            if (roomData == null)
                return;
                
            GetResponse().Init(PacketLibraryManager.OutgoingRequest("453"));
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
                
            Yupi.GetGame().GetNavigator().EnableNewNavigator(Session);
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
            
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNewNavigator(name, junk, Session));
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
                
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorSavedSearchesComposer"));
            messageBuffer.AppendInteger(Session.GetHabbo().NavigatorLogs.Count);
            
            foreach (UserSearchLog navi in Session.GetHabbo().NavigatorLogs.Values)
            {
                messageBuffer.AppendInteger(navi.Id);
                messageBuffer.AppendString(navi.Value1);
                messageBuffer.AppendString(navi.Value2);
                messageBuffer.AppendString(string.Empty);
            }
            
            Session.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     Serializes the saved search.
        /// </summary>
        /// <param name="textOne">The text one.</param>
        /// <param name="textTwo">The text two.</param>
        internal void SerializeSavedSearch(string textOne, string textTwo)
        {
            GetResponse().AppendString(textOne);
            GetResponse().AppendString(textTwo);
            GetResponse().AppendString(string.Empty);
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
            Session.GetHabbo().Preferences.NewnaviWidth = width;
            Session.GetHabbo().Preferences.NewnaviHeight = height;
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
            
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("NavigatorSavedSearchesComposer"));
            messageBuffer.AppendInteger(Session.GetHabbo().NavigatorLogs.Count);
            
            foreach (UserSearchLog navi in Session.GetHabbo().NavigatorLogs.Values)
            {
                messageBuffer.AppendInteger(navi.Id);
                messageBuffer.AppendString(navi.Value1);
                messageBuffer.AppendString(navi.Value2);
                messageBuffer.AppendString(string.Empty);
            }
            
            Session.SendMessage(messageBuffer);
        }

        /// <summary>
        ///     News the navigator collapse category.
        /// </summary>
        internal void NewNavigatorCollapseCategory() =>  Request.GetString();

        /// <summary>
        ///     News the navigator uncollapse category.
        /// </summary>
        internal void NewNavigatorUncollapseCategory() => Request.GetString();

        /// <summary>
        ///     Gets the pubs.
        /// </summary>
        internal void GetPubs()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializePublicRooms());
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
            GetResponse().Init(PacketLibraryManager.OutgoingRequest("1491"));
            
            GetResponse().AppendInteger(0);
            roomData.Serialize(GetResponse());
            
            SendResponse();
        }

        /// <summary>
        ///     Gets the popular rooms.
        /// </summary>
        internal void GetPopularRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, int.Parse(Request.GetString())));
        }

        /// <summary>
        ///     Gets the recommended rooms.
        /// </summary>
        internal void GetRecommendedRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, -1));
        }

        /// <summary>
        ///     Gets the popular groups.
        /// </summary>
        internal void GetPopularGroups()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, -2));
        }

        /// <summary>
        ///     Gets the high rated rooms.
        /// </summary>
        internal void GetHighRatedRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, -2));
        }

        /// <summary>
        ///     Gets the friends rooms.
        /// </summary>
        internal void GetFriendsRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, -4));
        }

        /// <summary>
        ///     Gets the rooms with friends.
        /// </summary>
        internal void GetRoomsWithFriends()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, -5));
        }

        /// <summary>
        ///     Gets the own rooms.
        /// </summary>
        internal void GetOwnRooms()
        {
            if (Session == null || Session.GetHabbo() == null)
                return;

            if (Session.GetHabbo().OwnRoomsSerialized == false)
            {
                Session.GetHabbo().UpdateRooms();
                
                Session.GetHabbo().OwnRoomsSerialized = true;
            }

            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNavigator(Session, -3));
        }

        /// <summary>
        ///     News the navigator flat cats.
        /// </summary>
        internal void NewNavigatorFlatCats()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeNewFlatCategories());
        }

        /// <summary>
        ///     Gets the favorite rooms.
        /// </summary>
        internal void GetFavoriteRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeFavoriteRooms(Session));
        }

        /// <summary>
        ///     Gets the recent rooms.
        /// </summary>
        internal void GetRecentRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializeRecentRooms(Session));
        }

        /// <summary>
        ///     Gets the popular tags.
        /// </summary>
        internal void GetPopularTags()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(Yupi.GetGame().GetNavigator().SerializePopularRoomTags());
        }

        /// <summary>
        ///     Gets the event rooms.
        /// </summary>
        internal void GetEventRooms()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(HotelBrowserManager.SerializePromoted(Session, Request.GetInteger()));
        }

        /// <summary>
        ///     Performs the search.
        /// </summary>
        internal void PerformSearch()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Session.SendMessage(HotelBrowserManager.SerializeSearchResults(Request.GetString()));
        }

        /// <summary>
        ///     Searches the by tag.
        /// </summary>
        internal void SearchByTag()
        {
            if (Session.GetHabbo() == null)
                return;
            
            // What's this Code Above?
            //this.Session.SendMessage(MercuryEnvironment.GetGame().GetNavigator().SerializeSearchResults(string.Format("tag:{0}", this.Request.PopFixedString())));
        }

        /// <summary>
        ///     Performs the search2.
        /// </summary>
        internal void PerformSearch2()
        {
            if (Session.GetHabbo() == null)
                return;
                
            Request.GetInteger();
            
            // What's this Code Above?
            //this.Session.SendMessage(MercuryEnvironment.GetGame().GetNavigator().SerializeSearchResults(this.Request.PopFixedString()));
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
                PublicItem pubItem = Yupi.GetGame().GetNavigator().GetPublicItem(roomId);
                
                if (pubItem == null) // Isn't A Staff Pick Room
                {
                    queryReactor.SetQuery("INSERT INTO navigator_publics (bannertype, room_id, category_parent_id) VALUES ('0', @roomId, '-2')");
                    
                    queryReactor.AddParameter("roomId", room.RoomId);
                    
                    uint lastInsertId = (uint) queryReactor.InsertQuery();
                    
                    PublicItem publicItem = new PublicItem(lastInsertId, 0, string.Empty, string.Empty, string.Empty, PublicImageType.Internal, room.RoomId, 0, -2, false, 1, string.Empty);
                    
                    Yupi.GetGame().GetNavigator().AddPublicItem(publicItem);
                }
                else // Is a Staff Pick Room
                {
                    queryReactor.SetQuery("DELETE FROM navigator_publics WHERE id = @pubId");
                    
                    queryReactor.AddParameter("pubId", pubItem.Id);
                    queryReactor.RunQuery();
                    
                    Yupi.GetGame().GetNavigator().RemovePublicItem(pubItem.Id);
                }
                
                room.RoomData.SerializeRoomData(Response, Session, false, true);
                
                Yupi.GetGame().GetNavigator().LoadNewPublicRooms();
            }
        }
    }
}
