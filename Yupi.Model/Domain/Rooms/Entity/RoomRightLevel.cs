// ---------------------------------------------------------------------------------
// <copyright file="RoomRightLevel.cs" company="https://github.com/sant0ro/Yupi">
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
namespace Yupi.Model
{
    using System;

    using Headspring;

    /// <summary>
    /// Room right level
    /// </summary>
    public class RoomRightLevel : Enumeration<RoomRightLevel>, IStatusString
    {
        #region Fields

        /// <summary>
        /// Rights as group admin
        /// </summary>
        public static readonly RoomRightLevel Group_Admin = new RoomRightLevel(3, "Group_Admin");

        /// <summary>
        /// Rights through group
        /// </summary>
        public static readonly RoomRightLevel Group_Rights = new RoomRightLevel(2, "Group_Rights");

        /// <summary>
        /// Moderator rights
        /// </summary>
        public static readonly RoomRightLevel Moderator = new RoomRightLevel(5, "Moderator");

        /// <summary>
        /// No Rights
        /// </summary>
        public static readonly RoomRightLevel None = new RoomRightLevel(0, "None");

        /// <summary>
        /// Room Owner rights
        /// </summary>
        public static readonly RoomRightLevel Owner = new RoomRightLevel(4, "Owner");

        /// <summary>
        /// Rights given by room owner
        /// </summary>
        public static readonly RoomRightLevel Rights = new RoomRightLevel(1, "Rights");

        #endregion Fields

        #region Constructors

        protected RoomRightLevel(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors

        #region Methods

        public string ToStatusString()
        {
            return "flatctrl " + Value;
        }

        #endregion Methods
    }
}