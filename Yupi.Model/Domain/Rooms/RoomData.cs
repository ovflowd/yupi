// ---------------------------------------------------------------------------------
// <copyright file="RoomData.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
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
        #region Properties

        [Required]
        public virtual bool AllowPets {
            get; set;
        } = true;

        [Required]
        public virtual bool AllowPetsEating {
            get; set;
        } = true;

        [Required]
        public virtual bool AllowWalkThrough {
            get; set;
        } = false;

        // TODO Implement bans with time & expire on server ticks?
        [ManyToMany]
        public virtual IList<UserInfo> BannedUsers {
            get; protected set;
        } = new List<UserInfo> ();

        /// <summary>
        ///     Room Category
        /// </summary>
        [Required]
        public virtual NavigatorCategory Category
        {
            get; set;
        }

        // TODO What are those exactly? (Format!)
        [Required]
        public virtual string CCTs {
            get; set;
        } = string.Empty;

        [Required]
        public virtual RoomChatSettings Chat {
            get; protected set;
        } = new RoomChatSettings ();

        [OneToMany]
        public virtual IList<ChatMessage> Chatlog {
            get; protected set;
        } = new List<ChatMessage> ();

        [Required]
        [Length (100)]
        public virtual string Description {
            get; set;
        } = string.Empty;

        public virtual RoomEvent Event
        {
            get; set;
        }

        [Required]
        public virtual float Floor {
            get; set;
        } = 0;

        // TODO Enum
        [Required]
        public virtual int FloorThickness {
            get; set;
        } = 0;

        public virtual Group Group
        {
            get; set;
        }

        [Required]
        public virtual bool HideWall {
            get; set;
        } = false;

        [Key]
        public virtual int Id
        {
            get; protected set;
        }

        [Required]
        public virtual bool IsMuted {
            get; set;
        } = false;

        [Required]
        public virtual bool IsPublic {
            get; set;
        } = false;

        // Enum?
        [Required]
        public virtual float LandScape {
            get; set;
        } = 0;

        [Required]
        public virtual RoomModel Model {
            get; set;
        } = RoomModel.Model_a;

        [Required]
        public virtual ModerationSettings ModerationSettings {
            get; protected set;
        }

        [OneToMany]
        public virtual IList<RoomMute> MutedEntities {
            get; protected set;
        } = new List<RoomMute> ();

        [Required]
        [Length(30)]
        public virtual string Name
        {
            get; set;
        }

        [Required]
        public virtual string NavigatorImage {
            get; set;
        } = string.Empty;

        [Required]
        public virtual UserInfo Owner
        {
            get; set;
        }

        [Required]
        public virtual string Password {
            get; set;
        } = string.Empty;

        public virtual IList<UserInfo> Rights {
            get; protected set;
        } = new List<UserInfo> ();

        /// <summary>
        ///     Room Score
        /// </summary>
        public virtual int Score
        {
            get; set;
        }

        public virtual SongMachineComponent SongMachine {
            get; protected set;
        } = new SongMachineComponent ();

        /// <summary>
        ///     Room Locked State
        /// </summary>
        public virtual RoomState State {
            get; set;
        } = RoomState.Open;

        [OneToMany]
        public virtual IList<Tag> Tags
        {
            get; protected set;
        } = new List<Tag>();

        [Required]
        public virtual TradingState TradeState {
            get; set;
        } = TradingState.NotAllowed;

        public virtual int UsersMax {
            get; set;
        } = 10;

        // TODO Isn't this part of the model?
        public virtual int WallHeight {
            get; set;
        } = 0;

        // TODO Determine proper type!
        public virtual float WallPaper {
            get; set;
        } = 0;

        // TODO Enum
        public virtual int WallThickness {
            get; set;
        } = 0;

        /// <summary>
        ///     Room Private Black Words
        /// </summary>
        [OneToMany]
        public virtual ISet<string> WordFilter {
            get; protected set;
        } = new HashSet<string> ();

        #endregion Properties

        #region Constructors

        public RoomData()
        {
            ModerationSettings = new ModerationSettings(this);
        }

        #endregion Constructors

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

            if (!IsPublic)
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