// ---------------------------------------------------------------------------------
// <copyright file="UserAction.cs" company="https://github.com/sant0ro/Yupi">
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

    using Headspring;

    public class UserAction : Enumeration<UserAction>
    {
        #region Fields

        public static readonly UserAction Blow = new UserAction(2, "Blow");
        public static readonly UserAction Idle = new UserAction(5, "Idle");
        public static readonly UserAction Jump = new UserAction(6, "Jump");
        public static readonly UserAction Laugh = new UserAction(3, "Laugh");
        public static readonly UserAction None = new UserAction(0, "None");
        public static readonly UserAction Respect = new UserAction(7, "Respect");

        // TODO What is this? (Maybe unused/legacy)
        public static readonly UserAction Unknown = new UserAction(4, "Unknown");
        public static readonly UserAction Wave = new UserAction(1, "Wave");

        #endregion Fields

        #region Constructors

        protected UserAction(int value, string displayName)
            : base(value, displayName)
        {
        }

        #endregion Constructors
    }
}