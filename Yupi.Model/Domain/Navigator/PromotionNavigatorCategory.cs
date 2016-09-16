// ---------------------------------------------------------------------------------
// <copyright file="PromotionNavigatorCategory.cs" company="https://github.com/sant0ro/Yupi">
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

    public class PromotionNavigatorCategory : NavigatorCategory, IPopulate
    {
        #region Properties

        // TODO Can this be moved up?
        public virtual bool Visible { get; set; }

        #endregion Properties

        protected PromotionNavigatorCategory() : base()
        {
        }

        public PromotionNavigatorCategory(string caption) : base(caption)
        {
        }


        public virtual void Populate()
        {
            ModelHelper.PopulateObject(
                new PromotionNavigatorCategory("${roomevent_type_0}"),
                new PromotionNavigatorCategory("${roomevent_type_1}"),
                new PromotionNavigatorCategory("${roomevent_type_2}"),
                new PromotionNavigatorCategory("${roomevent_type_3}"),
                new PromotionNavigatorCategory("${roomevent_type_4}"),
                new PromotionNavigatorCategory("${roomevent_type_5}"),
                new PromotionNavigatorCategory("${roomevent_type_6}"),
                new PromotionNavigatorCategory("${roomevent_type_7}"),
                new PromotionNavigatorCategory("${roomevent_type_8}"),
                new PromotionNavigatorCategory("${roomevent_type_9}"),
                new PromotionNavigatorCategory("${roomevent_type_10}"),
                new PromotionNavigatorCategory("${roomevent_type_11}"),
                new PromotionNavigatorCategory("Staffs")
                {
                    MinRank = 6 // TODO I don't like these hardcoded ranks
                }
            );
        }
    }
}