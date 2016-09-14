// ---------------------------------------------------------------------------------
// <copyright file="RoomEvent.cs" company="https://github.com/sant0ro/Yupi">
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

    /// <summary>
    ///     Class RoomEvent.
    /// </summary>
    public class RoomEvent
    {
        #region Properties

        /// <summary>
        ///     The category
        /// </summary>
        public virtual int Category
        {
            get; set;
        }

        // TODO What is the category? Foreing key!
        /// <summary>
        ///     The description
        /// </summary>
        public virtual string Description
        {
            get; set;
        }

        /// <summary>
        ///     The time
        /// </summary>
        public virtual DateTime ExpiresAt
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        /// <summary>
        ///     The name
        /// </summary>
        public virtual string Name
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public RoomEvent()
        {
            ExpiresAt = new DateTime().AddHours(2);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Gets a value indicating whether this instance has expired.
        /// </summary>
        /// <value><c>true</c> if this instance has expired; otherwise, <c>false</c>.</value>
        public virtual bool HasExpired()
        {
            return ExpiresAt < DateTime.Now;
        }

        #endregion Methods
    }
}