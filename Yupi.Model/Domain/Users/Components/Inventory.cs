// ---------------------------------------------------------------------------------
// <copyright file="Inventory.cs" company="https://github.com/sant0ro/Yupi">
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
    using System.Collections.Generic;
    using System.Linq;

    public class Inventory
    {
        #region Properties

        public virtual IList<FloorItem> FloorItems
        {
            get; protected set;
        }

        public virtual IList<PetItem> Pets
        {
            get; protected set;
        }

        public virtual IList<WallItem> WallItems
        {
            get; protected set;
        }

        [OneToMany]
        public virtual IList<WardrobeItem> Wardrobe
        {
            get; protected set;
        }

        #endregion Properties

        #region Constructors

        public Inventory()
        {
            this.Wardrobe = new List<WardrobeItem>();
            this.Pets = new List<PetItem>();
            this.FloorItems = new List<FloorItem>();
            this.WallItems = new List<WallItem>();
        }

        #endregion Constructors

        #region Methods

        // TODO Use visitor pattern to achieve this?
        public virtual void Add(Item item)
        {
            if (item is FloorItem)
            {
                FloorItems.Add((FloorItem) item);
            }
            else if (item is WallItem)
            {
                WallItems.Add((WallItem) item);
            }
            else if (item is PetItem)
            {
                Pets.Add((PetItem) item);
            }
        }

        public virtual FloorItem GetFloorItem(int id)
        {
            return FloorItems.SingleOrDefault(x => x.Id == id);
        }

        #endregion Methods
    }
}