#region Header

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

#endregion Header

namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;

    using Headspring;

    using Yupi.Model.Domain.Components;

    public class RoomData
    {
        #region Constructors

        public RoomData()
        {
            // TODO Should be removed...
            Name = "Unknown Room";
            Description = string.Empty;
            Type = "private";
            Tags = new List<string>();
            AllowPets = true;
            AllowPetsEating = false;
            AllowWalkThrough = true;
            HideWall = false;
            AllowRightsOverride = false;
            TradeState = TradingState.NotAllowed;
            WordFilter = new List<string>();
            Rights = new List<UserInfo>();
            MutedEntities = new List<RoomMute>();
            Chat = new RoomChatSettings();
            BannedUsers = new List<UserInfo>();
            WallHeight = -1;
            Chatlog = new List<ChatMessage>();
            ModerationSettings = new ModerationSettings(this);
            State = RoomState.Open;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Allow Pets in Room
        /// </summary>
        public virtual bool AllowPets
        {
            get; set;
        }

        /// <summary>
        ///     Allow Other Users Pets Eat in Room
        /// </summary>
        public virtual bool AllowPetsEating
        {
            get; set;
        }

        /// <summary>
        ///     Allow Override Room Rights
        /// </summary>
        public virtual bool AllowRightsOverride
        {
            get; set;
        }

        /// <summary>
        ///     Allow Users Walk Through Other Users
        /// </summary>
        public virtual bool AllowWalkThrough
        {
            get; set;
        }

        // TODO Implement bans with time & expire on server ticks?
        public virtual IList<UserInfo> BannedUsers
        {
            get; protected set;
        }

        /// <summary>
        ///     Room Category
        /// </summary>
        public virtual NavigatorCategory Category
        {
            get; set;
        }

        // TODO What are those exactly? (Format!)
        public virtual string CCTs
        {
            get; set;
        }

        public virtual RoomChatSettings Chat
        {
            get; protected set;
        }

        public virtual IList<ChatMessage> Chatlog
        {
            get; protected set;
        }

        /// <summary>
        ///     Room Description
        /// </summary>
        public virtual string Description
        {
            get; set;
        }

        public virtual RoomEvent Event
        {
            get; set;
        }

        public virtual float Floor
        {
            get; set;
        }

        public virtual int FloorThickness
        {
            get; set;
        }

        public virtual Group Group
        {
            get; set;
        }

        /// <summary>
        ///     Hide Wall in Room
        /// </summary>
        public virtual bool HideWall
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual bool IsMuted
        {
            get; set;
        }

        [Ignore]
        public virtual bool IsPublic
        {
            get
            {
                // TODO Remove Type!
                return Type != "private";
            }
        }

        public virtual float LandScape
        {
            get; set;
        }

        public virtual RoomModel Model
        {
            get; set;
        }

        public virtual ModerationSettings ModerationSettings
        {
            get; protected set;
        }

        public virtual IList<RoomMute> MutedEntities
        {
            get; protected set;
        }

        /// <summary>
        ///     Room Name
        /// </summary>
        public virtual string Name
        {
            get; set;
        }

        public virtual string NavigatorImage
        {
            get; set;
        }

        /// <summary>
        ///     Room Owner
        /// </summary>
        public virtual UserInfo Owner
        {
            get; set;
        }

        /// <summary>
        ///     Room Password
        /// </summary>
        public virtual string Password
        {
            get; set;
        }

        public virtual IList<UserInfo> Rights
        {
            get; protected set;
        }

        /// <summary>
        ///     Room Score
        /// </summary>
        public virtual int Score
        {
            get; set;
        }

        public virtual SongMachineComponent SongMachine
        {
            get; protected set;
        }

        /// <summary>
        ///     Room Locked State
        /// </summary>
        public virtual RoomState State
        {
            get; set;
        }

        /// <summary>
        ///     Room Tags
        /// </summary>
        [OneToMany]
        public virtual IList<string> Tags
        {
            get; protected set;
        }

        public virtual TradingState TradeState
        {
            get; set;
        }

        // TODO Enum private/public
        public virtual string Type
        {
            get; set;
        }

        /// <summary>
        ///     Max Amount of Users on Room
        /// </summary>
        public virtual int UsersMax
        {
            get; set;
        }

        // TODO Isn't this part of the model?
        public virtual int WallHeight
        {
            get; set;
        }

        // TODO Determine proper type!
        public virtual float WallPaper
        {
            get; set;
        }

        // TODO Enum
        public virtual int WallThickness
        {
            get; set;
        }

        /// <summary>
        ///     Room Private Black Words
        /// </summary>
        [OneToMany]
        public virtual IList<string> WordFilter
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public virtual RoomFlags GetFlags()
        {
            RoomFlags flag = RoomFlags.Default;

            if (NavigatorImage != null)
            {
                flag |= RoomFlags.Image;
            }

            if (Group != null)
            {
                flag |= RoomFlags.Group;
            }

            if (Event != null)
            {
                flag |= RoomFlags.Event;
            }

            if (Type == "private")
            {
                flag |= RoomFlags.Private;
            }

            if (AllowPets)
            {
                flag |= RoomFlags.AllowPets;
            }
            return flag;
        }

        public virtual bool HasOwnerRights(UserInfo user)
        {
            return (Owner == user || user.HasPermission("fuse_any_room_controller"));
        }

        public virtual bool HasRights(UserInfo user)
        {
            return (Owner == user || user.HasPermission("fuse_any_rooms_rights") || Rights.Contains(user));
        }

        #endregion Methods

        #region Other

        // TODO Remove when not used anymore
        /*
        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="messageBuffer">The messageBuffer.</param>
        /// <param name="showEvents">if set to <c>true</c> [show events].</param>
        /// <param name="enterRoom"></param>
         public void Serialize(SimpleServerMessageBuffer messageBuffer, bool showEvents = false, bool enterRoom = false)
        {
            messageBuffer.AppendInteger(data.Id);
            messageBuffer.AppendString(data.Name);
            messageBuffer.AppendInteger(data.OwnerId);
            messageBuffer.AppendString(data.Owner);
            messageBuffer.AppendInteger(data.State);
            messageBuffer.AppendInteger(data.UsersNow);
            messageBuffer.AppendInteger(data.UsersMax);
            messageBuffer.AppendString(data.Description);
            messageBuffer.AppendInteger(data.TradeState);
            messageBuffer.AppendInteger(data.Score);
            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(data.Category > 0 ? data.Category : 0);
            messageBuffer.AppendInteger(data.TagCount);

            foreach (string current in data.Tags.Where(current => current != null))
                messageBuffer.AppendString(current);

            string imageData = null;

            int enumType = enterRoom ? 32 : 0;

            PublicItem publicItem = Yupi.GetGame()?.GetNavigator()?.GetPublicRoom(data.Id);

            if (!string.IsNullOrEmpty(publicItem?.Image))
            {
                imageData = publicItem.Image;

                enumType += 1;
            }

            if (data.Group != null)
                enumType += 2;

            if (showEvents && data.Event != null)
                enumType += 4;

            if (data.Type == "private")
                enumType += 8;

            if (data.AllowPets)
                enumType += 16;

            messageBuffer.AppendInteger(enumType);

            if (imageData != null)
                messageBuffer.AppendString(imageData);

            if (data.Group != null)
            {
                messageBuffer.AppendInteger(data.Group.Id);
                messageBuffer.AppendString(data.Group.Name);
                messageBuffer.AppendString(data.Group.Badge);
            }

            if (showEvents && data.Event != null)
            {
                messageBuffer.AppendString(data.Event.Name);
                messageBuffer.AppendString(data.Event.Description);
                messageBuffer.AppendInteger((int)Math.Floor((data.Event.Time - Yupi.GetUnixTimeStamp()) / 60.0));
            }
        }*/

        #endregion Other
    }

    public class RoomState : Enumeration<RoomState>
    {
        #region Fields

        public static readonly RoomState Bell = new RoomState(1, "Bell");

        /// <summary>
        /// Invisible in navigator to users without rights
        /// </summary>
        public static readonly RoomState Invisible = new RoomState(3, "Invisible");
        public static readonly RoomState Locked = new RoomState(2, "Locked");
        public static readonly RoomState Open = new RoomState(0, "Open");

        #endregion Fields

        #region Constructors

        public RoomState(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}