// ---------------------------------------------------------------------------------
// <copyright file="UserPreferences.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Data;

    public class UserPreferences
    {
        #region Properties

        [Required]
        public virtual ChatBubbleStyle ChatBubbleStyle {
            get; set;
        } = ChatBubbleStyle.Normal;

        [Required]
        public virtual bool DisableCameraFollow {
            get; set;
        } = false;

        /// <summary>
        ///     Ignore Room Invitations
        /// </summary>
        [Required]
        public virtual bool IgnoreRoomInvite {
            get; set;
        } = false;

        /// <summary>
        ///     Navigator Height
        /// </summary>
        [Required]
        public virtual int NavigatorHeight {
            get; set;
        } = 600;

        /// <summary>
        ///     Navigator Width
        /// </summary>
        [Required]
        public virtual int NavigatorWidth {
            get; set;
        } = 580;

        /// <summary>
        ///     Navigator Position X
        /// </summary>
        [Required]
        public virtual int NewnaviX {
            get; set;
        } = 0;

        /// <summary>
        ///     Navigator Position Y
        /// </summary>
        [Required]
        public virtual int NewnaviY {
            get; set;
        } = 0;

        /// <summary>
        ///     User Prefers Old Chat
        /// </summary>
        [Required]
        public virtual bool PreferOldChat {
            get; set;
        } = false;

        // TODO What do the single values mean? + Validation!
        [Required]
        public virtual int Volume1 {
            get; set;
        } = 100;

        [Required]
        public virtual int Volume2 {
            get; set;
        } = 100;

        [Required]
        public virtual int Volume3 {
            get; set;
        } = 100;

        #endregion Properties
    }
}