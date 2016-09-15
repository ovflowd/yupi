// ---------------------------------------------------------------------------------
// <copyright file="HotelLandingPromos.cs" company="https://github.com/sant0ro/Yupi">
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
using Yupi.Model.Domain.Components;
using System;


namespace Yupi.Model.Domain
{
    public class HotelLandingPromos
    {
        #region Properties

        public virtual string Body
        {
            get; set;
        }

        public virtual string Button
        {
            get; set;
        }

        public virtual string Title
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual string Image
        {
            get; set;
        }
            
        public virtual PromoLink Link
        {
            get; set;
        }

        public virtual DateTime CreatedAt { get; set; }

        #endregion Properties

        public HotelLandingPromos()
        {
            CreatedAt = DateTime.Now;
        }
    }
}