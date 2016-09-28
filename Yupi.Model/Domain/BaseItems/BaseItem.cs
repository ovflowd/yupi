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
using System.Drawing;
using System;
using System.Runtime.Serialization;


namespace Yupi.Model.Domain
{
    using System.Collections.Generic;

    [IsDiscriminated]
    [Serializable]
    public abstract class BaseItem
    {
        #region Properties

        public virtual bool AllowInventoryStack
        {
            get; set;
        }

        public virtual bool AllowMarketplaceSell
        {
            get; set;
        }
            
        public virtual bool AllowRecycle
        {
            get; set;
        }
            
        public virtual bool AllowTrade
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

        public virtual int DimensionX
        {
            get; set;
        }
            
        public virtual int DimensionY
        {
            get; set;
        }

        [Nullable]
        public virtual string AdUrl
        {
            get; set;
        }
            
        public virtual string Classname
        {
            get; set;
        }
            
        public virtual TString Name
        {
            get; set;
        }

        public virtual TString Description
        {
            get; set;
        }
            
        public virtual bool Stackable
        {
            get; set;
        }
            
        public virtual float Height
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
            
        [Ignore]
        public virtual bool CanWalkOn
        {
            get {
                return false;
            }
        }

        [Ignore]
        public virtual bool CanLayOn
        {
            get {
                return false;
            }
        }

        [Ignore]
        public virtual bool CanSitOn
        {
            get {
                return false;
            }
        }

        public virtual int Revision
        {
            get; set;
        }


        [Ignore]
        public virtual bool ExcludeFromSearch
        {
            get {
                return false;
            }
        }

        [Nullable]
        public virtual string FurniLine
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public abstract Item CreateNew();

        #endregion Methods
    }
}