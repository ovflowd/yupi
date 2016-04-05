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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using Yupi.Emulator.Core.Io.Logger;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Rooms.Chat;
using Yupi.Emulator.Game.Rooms.Data.Composers;
using Yupi.Emulator.Game.Rooms.Events.Models;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Data.Models
{
    /// <summary>
    ///     Class GetPublicRoomData.
    /// </summary>
     class RoomData
    {
        /// <summary>
        ///     Room Model
        /// </summary>
        private RoomModel _model;

        /// <summary>
        ///     Allow Pets in Room
        /// </summary>
         bool AllowPets;

        /// <summary>
        ///     Allow Other Users Pets Eat in Room
        /// </summary>
         bool AllowPetsEating;

        /// <summary>
        ///     Allow Users Walk Through Other Users
        /// </summary>
         bool AllowWalkThrough;

        /// <summary>
        ///     Hide Wall in Room
        /// </summary>
         bool HideWall;

        /// <summary>
        ///     Allow Override Room Rights
        /// </summary>
         bool AllowRightsOverride;

        /// <summary>
        ///     Room Category
        /// </summary>
         int Category;

        /// <summary>
        ///     What the Hell?
        /// </summary>
         string CcTs;

        /// <summary>
        ///     Chat Balloon Type
        /// </summary>
         uint ChatBalloon;

        /// <summary>
        ///     Chat Walk Speed
        /// </summary>
         uint ChatSpeed;

        /// <summary>
        ///     Chat Max Distance each Other Message
        /// </summary>
         uint ChatMaxDistance;

        /// <summary>
        ///     Chat Flood Protection
        /// </summary>
         uint ChatFloodProtection;

        /// <summary>
        ///     Room Chat Type
        /// </summary>
         int ChatType;

        /// <summary>
        ///     Is Room In Competition
        /// </summary>
         int CompetitionStatus;

        /// <summary>
        ///     Amount of Votes of the Room
        /// </summary>
         int CompetitionVotes;

        /// <summary>
        ///     Room Description
        /// </summary>
         string Description;

        /// <summary>
        ///     Disable Pull Users in Room
        /// </summary>
         bool DisablePull = false;

        /// <summary>
        ///     Disable Push Users in Room
        /// </summary>
         bool DisablePush = false;

        /// <summary>
        ///     Room Event
        /// </summary>
         RoomEvent Event;

        /// <summary>
        ///     Floor String?
        /// </summary>
         string Floor;

        /// <summary>
        ///     Room Flor Thickness
        /// </summary>
         int FloorThickness;

        /// <summary>
        ///     Room Game Identifier
        /// </summary>
         int GameId;

        /// <summary>
        ///     Room Group
        /// </summary>
         Group Group;

        /// <summary>
        ///     Room Group Id
        /// </summary>
         uint GroupId;

        /// <summary>
        ///     Room Id
        /// </summary>
         uint Id;

        /// <summary>
        ///     Landscape String
        /// </summary>
         string LandScape;

        /// <summary>
        ///     Last Time when Room was Used
        /// </summary>
        public DateTime LastUsed = DateTime.Now;

        /// <summary>
        ///     Room Model Name
        /// </summary>
         string ModelName;

        /// <summary>
        ///     Room Name
        /// </summary>
         string Name;

        /// <summary>
        ///     Room Owner
        /// </summary>
         string Owner;

        /// <summary>
        ///     Room Owner Id
        /// </summary>
         uint OwnerId;

        /// <summary>
        ///     Room Password
        /// </summary>
         string PassWord;

        /// <summary>
        ///     Room Chat Log
        /// </summary>
         ConcurrentStack<Chatlog> RoomChat;

        /// <summary>
        ///     Room Score
        /// </summary>
         int Score;

        /// <summary>
        ///     Room Locked State
        /// </summary>
         int State;

        /// <summary>
        ///     Room Tags
        /// </summary>
         List<string> Tags;

        /// <summary>
        ///     Room Trade State
        /// </summary>
         int TradeState;

        /// <summary>
        ///     Room Type
        /// </summary>
         string Type;

        /// <summary>
        ///     Amount of Users on Room
        /// </summary>
         uint UsersNow;

        /// <summary>
        ///     Max Amount of Users on Room
        /// </summary>
         uint UsersMax;

        /// <summary>
        ///     Room Wall Height
        /// </summary>
         int WallHeight;

        /// <summary>
        ///     The wall paper
        /// </summary>
         string WallPaper;

        /// <summary>
        ///     Room Wall Tchickness
        /// </summary>
         int WallThickness;

        /// <summary>
        ///     Who Can Ban Users in Room
        /// </summary>
         int WhoCanBan;

        /// <summary>
        ///     Who Can Kick Users in Room
        /// </summary>
         int WhoCanKick;

        /// <summary>
        ///     Who Can Mute Users in Room
        /// </summary>
         int WhoCanMute;

        /// <summary>
        ///     Room Private Black Words
        /// </summary>
         List<string> WordFilter;

        /// <summary>
        ///     Room Tags Count
        /// </summary>
        /// <value>The tag count.</value>
         int TagCount => Tags.Count;

        /// <summary>
        ///     Gets a value indicating whether this instance has event.
        /// </summary>
        /// <value><c>true</c> if this instance has event; otherwise, <c>false</c>.</value>
         bool HasEvent => false;

        /// <summary>
        ///     Gets the model.
        /// </summary>
        /// <value>The model.</value>
         RoomModel Model => _model ?? (_model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id));

        /// <summary>
        ///     Resets the model.
        /// </summary>
         void ResetModel() => _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);

        /// <summary>
        ///     Fills the null.
        /// </summary>
        /// <param name="id">The identifier.</param>
         void FillNull(uint id)
        {
            Id = id;
            Name = "Unknown Room";
            Description = "-";
            Type = "private";
            Owner = "-";
            Category = 0;
            UsersNow = 0;
            UsersMax = 0;
            ModelName = "NO_MODEL";
            CcTs = string.Empty;
            Score = 0;
            Tags = new List<string>();
            AllowPets = true;
            AllowPetsEating = false;
            AllowWalkThrough = true;
            HideWall = false;
            PassWord = string.Empty;
            WallPaper = "0.0";
            Floor = "0.0";
            LandScape = "0.0";
            WallThickness = 0;
            FloorThickness = 0;
            Group = null;
            AllowRightsOverride = false;
            Event = null;
            GameId = 0;
            WhoCanBan = 0;
            WhoCanKick = 0;
            WhoCanMute = 0;
            TradeState = 2;
            State = 0;
            RoomChat = new ConcurrentStack<Chatlog>();
            WordFilter = new List<string>();
            WallHeight = -1;
            _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);
            CompetitionStatus = 0;
        }

        /// <summary>
        ///     Fills the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
         void Fill(DataRow row)
        {
            try
            {
                Id = (uint) row["id"];

                Name = (string) row["caption"];
                PassWord = (string) row["password"];
                Description = (string) row["description"];
                Type = (string) row["roomtype"];

                Owner = string.Empty;

                OwnerId = (uint) row["owner"];

                RoomChat = new ConcurrentStack<Chatlog>();
                WordFilter = new List<string>();

                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery("SELECT username FROM users WHERE id = @userId");
                    queryReactor.AddParameter("userId", OwnerId);

                    Owner = queryReactor.GetString();

                    queryReactor.SetQuery($"SELECT user_id, message, timestamp FROM users_chatlogs WHERE room_id = '{Id}' ORDER BY timestamp ASC LIMIT 150");

                    DataTable table = queryReactor.GetTable();

                    if (table != null)
                    {
                        foreach (DataRow dataRow in table.Rows)
                            RoomChat.Push(new Chatlog((uint)dataRow[0], (string)dataRow[1], Yupi.UnixToDateTime(int.Parse(dataRow[2].ToString())), false));

                    }

                    queryReactor.SetQuery($"SELECT word FROM rooms_wordfilter WHERE room_id = '{Id}'");

                    DataTable tableFilter = queryReactor.GetTable();

                    if (tableFilter != null)
                    {
                        foreach (DataRow dataRow in tableFilter.Rows)
                            WordFilter.Add(dataRow["word"].ToString());
                    }
                }

                string roomState = row["state"].ToString().ToLower();

                switch (roomState)
                {
                    case "locked":
                        State = 1;
                        break;
                    case "password":
                        State = 2;
                        break;
                    default:
                        State = 0;
                        break;
                }

                ModelName = (string) row["model_name"];
                WallPaper = (string) row["wallpaper"];
                Floor = (string) row["floor"];
                LandScape = (string) row["landscape"];
                CcTs = (string) row["public_ccts"];

                int.TryParse(row["trade_state"].ToString(), out TradeState);
                int.TryParse(row["category"].ToString(), out Category);
                int.TryParse(row["walls_height"].ToString(), out WallHeight);
                int.TryParse(row["score"].ToString(), out Score);
                int.TryParse(row["floorthick"].ToString(), out FloorThickness);
                int.TryParse(row["wallthick"].ToString(), out WallThickness);
                int.TryParse(row["chat_type"].ToString(), out ChatType);
                int.TryParse(row["game_id"].ToString(), out GameId);
                int.TryParse(row["mute_settings"].ToString(), out WhoCanMute);
                int.TryParse(row["kick_settings"].ToString(), out WhoCanKick);
                int.TryParse(row["ban_settings"].ToString(), out WhoCanBan);

                uint.TryParse(row["users_now"].ToString(), out UsersNow);
                uint.TryParse(row["users_max"].ToString(), out UsersMax);
                uint.TryParse(row["group_id"].ToString(), out GroupId);
                uint.TryParse(row["chat_balloon"].ToString(), out ChatBalloon);
                uint.TryParse(row["chat_speed"].ToString(), out ChatSpeed);
                uint.TryParse(row["chat_max_distance"].ToString(), out ChatMaxDistance);
                uint.TryParse(row["chat_flood_protection"].ToString(), out ChatFloodProtection);

                AllowPets = Yupi.EnumToBool(row["allow_pets"].ToString());
                AllowPetsEating = Yupi.EnumToBool(row["allow_pets_eat"].ToString());
                AllowWalkThrough = Yupi.EnumToBool(row["allow_walkthrough"].ToString());
                HideWall = Yupi.EnumToBool(row["hidewall"].ToString());

                AllowRightsOverride = false;

                Group = Yupi.GetGame().GetGroupManager().GetGroup(GroupId);
                Event = Yupi.GetGame().GetRoomEvents().GetEvent(Id);
                _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);

                CompetitionStatus = 0;

                Tags = new List<string>();

                if (row.IsNull("tags") || string.IsNullOrEmpty(row["tags"].ToString()))
                    return;

                foreach (string item in row["tags"].ToString().Split(','))
                    Tags.Add(item);
            }
            catch (Exception ex)
            {
                YupiLogManager.LogException(ex, "Registered Room Serialization Exception.", "Yupi.Room");
            }
        }

        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="messageBuffer">The messageBuffer.</param>
        /// <param name="showEvents">if set to <c>true</c> [show events].</param>
        /// <param name="enterRoom"></param>
         void Serialize(SimpleServerMessageBuffer messageBuffer, bool showEvents = false, bool enterRoom = false) => RoomDataComposer.Serialize(messageBuffer, this, showEvents, enterRoom);

        /// <summary>
        ///     Serializes the room data.
        /// </summary>
        /// <param name="messageBuffer">The messageBuffer.</param>
        /// <param name="session">The session.</param>
        /// <param name="isNotReload">if set to <c>true</c> [from view].</param>
        /// <param name="sendRoom">if set to <c>true</c> [send room].</param>
        /// <param name="show">if set to <c>true</c> [show].</param>
         void SerializeRoomData(SimpleServerMessageBuffer messageBuffer, GameClient session, bool isNotReload, bool? sendRoom = false, bool show = true)
        {
            SimpleServerMessageBuffer roomDataBuffer = RoomDataComposer.Compose(messageBuffer, session, Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId), this, isNotReload, sendRoom, show);

            if (sendRoom == null)
                return;

            if (sendRoom.Value && Yupi.GetGame().GetRoomManager().GetRoom(Id) != null)
            {
                Yupi.GetGame().GetRoomManager().GetRoom(Id).SendMessage(roomDataBuffer);

                return;
            }

            session.SendMessage(roomDataBuffer);
        }
    }
}