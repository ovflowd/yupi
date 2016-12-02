// ---------------------------------------------------------------------------------
// <copyright file="UserBadgeComponent.cs" company="https://github.com/sant0ro/Yupi">
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

    using Yupi.Util;

    public class UserBadgeComponent
    {
        #region Properties

        [OneToMany]
        public virtual IList<UserBadge> Badges
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public virtual UserBadge GetBadge(string badgeCode)
        {
            return Badges.FirstOrDefault(x => x.Badge.Code == badgeCode);
        }

        public virtual IList<UserBadge> GetVisible()
        {
            return Badges.Where(x => x.Slot > 0).ToList();
        }

        public virtual void GiveBadge(Badge badge)
        {
            Badges.Add(new UserBadge()
            {
                Badge = badge
            });

            // TODO Update badges?!
        }

        public virtual bool HasBadge(string badeCode)
        {
            return Badges.Any(x => x.Badge.Code == badeCode);
        }

        public virtual bool RemoveBadge(Badge badge)
        {
            return Badges.RemoveAll((x) => x.Badge == badge) > 0;
        }

        public virtual void ResetSlots()
        {
            foreach (UserBadge badge in Badges)
            {
                badge.Slot = 0;
            }
        }

        #endregion Methods
    }
}