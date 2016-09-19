// ---------------------------------------------------------------------------------
// <copyright file="BaseItem.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;

    [IsDiscriminated]
    public abstract class BaseItem
    {
        #region Properties

        /// <summary>
        ///     The allow inventory stack
        /// </summary>
        public virtual bool AllowInventoryStack
        {
            get; set;
        }

        /// <summary>
        ///     The allow marketplace sell
        /// </summary>
        public virtual bool AllowMarketplaceSell
        {
            get; set;
        }

        /// <summary>
        ///     The allow recycle
        /// </summary>
        public virtual bool AllowRecycle
        {
            get; set;
        }

        /// <summary>
        ///     The allow trade
        /// </summary>
        public virtual bool AllowTrade
        {
            get; set;
        }

        /// <summary>
        ///     The height
        /// </summary>
        public virtual double Height
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        [Ignore]
        public virtual ItemSpecialType SpecialType
        {
            get {
                return ItemSpecialType.Default;
            }
        }

        /// <summary>
        ///     The length
        /// </summary>
        public virtual int Length
        {
            get; set;
        }

        /// <summary>
        ///     The name
        /// </summary>
        public virtual string Name
        {
            get; set;
        }

        /// <summary>
        ///     The public name
        /// </summary>
        public virtual string PublicName
        {
            get; set;
        }

        /// <summary>
        ///     The stackable
        /// </summary>
        public virtual bool Stackable
        {
            get; set;
        }

        /// <summary>
        ///     The stack multipler
        /// </summary>
        public virtual bool StackMultipler
        {
            get; set;
        }

        /// <summary>
        ///     The subscriber only
        /// </summary>
        public virtual bool SubscriberOnly
        {
            get; set;
        }

        /// <summary>
        ///     The toggle height
        /// </summary>
        public virtual double[] ToggleHeight
        {
            get; set;
        }

        [Ignore]
        public virtual ItemType Type
        {
            get { return ItemType.Floor; }
        }

        [Ignore]
        public virtual string Visualization
        {
            get {
                // TODO Implement
                return string.Empty;
            }
        }

        /// <summary>
        ///     The walkable
        /// </summary>
        public virtual bool Walkable
        {
            get; set;
        }

        /// <summary>
        ///     The width
        /// </summary>
        public virtual int Width
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public abstract Item CreateNew();

        #endregion Methods
    }
}