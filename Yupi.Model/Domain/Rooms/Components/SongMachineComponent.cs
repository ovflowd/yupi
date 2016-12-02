// ---------------------------------------------------------------------------------
// <copyright file="SongMachineComponent.cs" company="https://github.com/sant0ro/Yupi">
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

    public class SongMachineComponent
    {
        #region Properties

        [Ignore]
        public virtual int Capacity
        {
            get { return 20; // TODO What is the real capacity?
            }
        }

        public virtual List<SongItem> SongItems
        {
            get; set;
        } = new List<SongItem> ();

        #endregion Properties

        #region Methods

        public virtual SongItem Find(int id)
        {
            return SongItems.SingleOrDefault(x => x.Id == id);
        }

        public virtual void Remove(SongItem item)
        {
            SongItems.Remove(item);
        }

        public virtual bool TryAdd(SongItem item)
        {
            if (SongItems.Count < Capacity)
            {
                SongItems.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Methods
    }
}