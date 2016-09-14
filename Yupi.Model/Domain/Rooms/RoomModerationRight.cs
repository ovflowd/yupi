// ---------------------------------------------------------------------------------
// <copyright file="RoomModerationRight.cs" company="https://github.com/sant0ro/Yupi">
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

    public class RoomModerationRight : Enumeration<RoomModerationRight>
    {
        #region Fields

        public static readonly RoomModerationRight Everybody = new RoomModerationRight(2, "Everybody");
        public static readonly RoomModerationRight None = new RoomModerationRight(0, "None");
        public static readonly RoomModerationRight UsersWithRights = new RoomModerationRight(1, "UsersWithRights");

        #endregion Fields

        #region Constructors

        private RoomModerationRight(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}