// ---------------------------------------------------------------------------------
// <copyright file="ModerationSettings.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model.Domain.Components
{
    using System;

    public class ModerationSettings
    {
        #region Properties

        [Required]
        public virtual RoomModerationRight WhoCanBan
        {
            get; set;
        } = RoomModerationRight.None;

        [Required]
        public virtual RoomModerationRight WhoCanKick
        {
            get; set;
        } = RoomModerationRight.None;

        [Required]
        public virtual RoomModerationRight WhoCanMute
        {
            get; set;
        } = RoomModerationRight.None;

        [Required]
        protected virtual RoomData Room
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public ModerationSettings(RoomData room)
        {
            this.Room = room;
        }

        protected ModerationSettings()
        {
        }

        #endregion Constructors

        #region Methods

        public bool CanBan(UserInfo info)
        {
            if (this.WhoCanBan == RoomModerationRight.None)
            {
                return this.Room.HasOwnerRights(info);
            }
            else
            {
                return this.Room.HasRights(info);
            }
        }

        public bool CanKick(UserInfo info)
        {
            if (this.WhoCanKick == RoomModerationRight.None)
            {
                return this.Room.HasOwnerRights(info);
            }
            else if (this.WhoCanKick == RoomModerationRight.UsersWithRights)
            {
                return this.Room.HasRights(info);
            }
            else
            {
                return true;
            }
        }

        public bool CanMute(UserInfo info)
        {
            if (this.WhoCanMute == RoomModerationRight.None)
            {
                return this.Room.HasOwnerRights(info);
            }
            else
            {
                return this.Room.HasRights(info);
            }
        }

        #endregion Methods
    }
}