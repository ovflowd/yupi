// ---------------------------------------------------------------------------------
// <copyright file="FloorBaseItem.cs" company="https://github.com/sant0ro/Yupi">
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


namespace Yupi.Model.Domain
{
    using System;

    [Serializable]public class FloorBaseItem : BaseItem
    {
        #region Methods

        // TODO Enum
        public virtual int DefaultDir
        {
            get; set;
        }

        
        public virtual int[] InternalPartColors
        {
            get;
            protected set;
        }

        [Ignore]
        public virtual Color[] PartColors
        {
            get {
                Color[] colors = new Color[InternalPartColors.Length];
                for (int i = 0; i < InternalPartColors.Length; ++i)
                {
                    colors[i] = Color.FromArgb(InternalPartColors[i]);
                }
                return colors;
            }
            set {
                InternalPartColors = new int[value.Length];
                for (int i = 0; i < value.Length; ++i)
                {
                    InternalPartColors[i] = value[i].ToArgb();
                }
            }
        }

        public override Item CreateNew()
        {
            return new SimpleFloorItem()
            {
                BaseItem = this
            };
        }

        #endregion Methods
    }
}