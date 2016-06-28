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


namespace Yupi.Emulator.Game.Rooms.Data.Models
{
    /// <summary>
    ///     Class GetPublicRoomData.
    /// </summary>
     public class RoomData
    {
        /// <summary>
        ///     Room Model
        /// </summary>
        private RoomModel _model;

        /// <summary>
        ///     Allow Pets in Room
        /// </summary>
     public bool AllowPets;

        /// <summary>
        ///     Allow Other Users Pets Eat in Room
        /// </summary>
     public bool AllowPetsEating;

        /// <summary>
        ///     Allow Users Walk Through Other Users
        /// </summary>
     public bool AllowWalkThrough;

        /// <summary>
        ///     Hide Wall in Room
        /// </summary>
     public bool HideWall;

        /// <summary>
        ///     Allow Override Room Rights
        /// </summary>
     public bool AllowRightsOverride;

        /// <summary>
        ///     Room Category
        /// </summary>
     public int Category;

        /// <summary>
        ///     What the Hell?
        /// </summary>
     public string CcTs;

        /// <summary>
        ///     Chat Balloon Type
        /// </summary>
     public uint ChatBalloon;

        /// <summary>
        ///     Chat Walk Speed
        /// </summary>
     public uint ChatSpeed;

        /// <summary>
        ///     Chat Max Distance each Other Message
        /// </summary>
     public uint ChatMaxDistance;

        /// <summary>
        ///     Chat Flood Protection
        /// </summary>
     public uint ChatFloodProtection;

        /// <summary>
        ///     Room Chat Type
        /// </summary>
     public int ChatType;

        /// <summary>
        ///     Is Room In Competition
        /// </summary>
     public int CompetitionStatus;

        /// <summary>
        ///     Amount of Votes of the Room
        /// </summary>
     public int CompetitionVotes;

        /// <summary>
        ///     Room Description
        /// </summary>
     public string Description;

        /// <summary>
        ///     Disable Pull Users in Room
        /// </summary>
     public bool DisablePull = false;

        /// <summary>
        ///     Disable Push Users in Room
        /// </summary>
     public bool DisablePush = false;

        /// <summary>
        ///     Room Event
        /// </summary>
     public RoomEvent Event;

        /// <summary>
        ///     Floor String?
        /// </summary>
     public string Floor;

        /// <summary>
        ///     Room Flor Thickness
        /// </summary>
     public int FloorThickness;

        /// <summary>
        ///     Room Game Identifier
        /// </summary>
     public int GameId;

        /// <summary>
        ///     Room Group
        /// </summary>
     public Group Group;

        /// <summary>
        ///     Room Group Id
        /// </summary>
     public uint GroupId;

        /// <summary>
        ///     Room Id
        /// </summary>
     public uint Id;

        /// <summary>
        ///     Landscape String
        /// </summary>
     public string LandScape;

        /// <summary>
        ///     Last Time when Room was Used
        /// </summary>
        public DateTime LastUsed = DateTime.Now;

        /// <summary>
        ///     Room Model Name
        /// </summary>
     public string ModelName;

        /// <summary>
        ///     Room Name
        /// </summary>
     public string Name;

        /// <summary>
        ///     Room Owner
        /// </summary>
     public string Owner;

        /// <summary>
        ///     Room Owner Id
        /// </summary>
     public uint OwnerId;

        /// <summary>
        ///     Room Password
        /// </summary>
     public string PassWord;

        /// <summary>
        ///     Room Chat Log
        /// </summary>
     public ConcurrentStack<Chatlog> RoomChat;

        /// <summary>
        ///     Room Score
        /// </summary>
     public int Score;

        /// <summary>
        ///     Room Locked State
        /// </summary>
     public int State;

        /// <summary>
        ///     Room Tags
        /// </summary>
     public List<string> Tags;

        /// <summary>
        ///     Room Trade State
        /// </summary>
     public int TradeState;

        /// <summary>
        ///     Room Type
        /// </summary>
     public string Type;

        /// <summary>
        ///     Amount of Users on Room
        /// </summary>
     public uint UsersNow;

        /// <summary>
        ///     Max Amount of Users on Room
        /// </summary>
     public uint UsersMax;

        /// <summary>
        ///     Room Wall Height
        /// </summary>
     public int WallHeight;

        /// <summary>
        ///     The wall paper
        /// </summary>
     public string WallPaper;

        /// <summary>
        ///     Room Wall Tchickness
        /// </summary>
     public int WallThickness;

        /// <summary>
        ///     Who Can Ban Users in Room
        /// </summary>
     public int WhoCanBan;

        /// <summary>
        ///     Who Can Kick Users in Room
        /// </summary>
     public int WhoCanKick;

        /// <summary>
        ///     Who Can Mute Users in Room
        /// </summary>
     public int WhoCanMute;

        /// <summary>
        ///     Room Private Black Words
        /// </summary>
     public List<string> WordFilter;

        /// <summary>
        ///     Room Tags Count
        /// </summary>
        /// <value>The tag count.</value>
     public int TagCount => Tags.Count;

        /// <summary>
        ///     Gets a value indicating whether this instance has event.
        /// </summary>
        /// <value><c>true</c> if this instance has event; otherwise, <c>false</c>.</value>
     public bool HasEvent => false;

        /// <summary>
        ///     Gets the model.
        /// </summary>
        /// <value>The model.</value>
     public RoomModel Model => _model ?? (_model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id));

        /// <summary>
        ///     Resets the model.
        /// </summary>
     public void ResetModel() => _model = Yupi.GetGame().GetRoomManager().GetModel(ModelName, Id);

        /// <summary>
        ///     Fills the null.
        /// </summary>
        /// <param name="id">The identifier.</param>
     public void FillNull(uint id)
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
     public void Fill(DataRow row)
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
     public void Serialize(SimpleServerMessageBuffer messageBuffer, bool showEvents = false, bool enterRoom = false) 
		{
			RoomDataComposer.Serialize(messageBuffer, this, showEvents, enterRoom);
		}
    }
}