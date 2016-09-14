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
        public virtual IList<Badge> Badges
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public virtual Badge GetBadge(string badgeCode)
        {
            return Badges.FirstOrDefault(x => x.Code == badgeCode);
        }

        public virtual IList<Badge> GetVisible()
        {
            return Badges.Where(x => x.Slot > 0).ToList();
        }

        public virtual void GiveBadge(string badeCode)
        {
            Badges.Add(new Badge()
            {
                Code = badeCode
            });

            // TODO Update badges?!
        }

        public virtual bool HasBadge(string badeCode)
        {
            return Badges.Any(x => x.Code == badeCode);
        }

        public virtual bool RemoveBadge(string badeCode)
        {
            return Badges.RemoveAll((x) => x.Code == badeCode) > 0;
        }

        public virtual void ResetSlots()
        {
            foreach (Badge badge in Badges)
            {
                badge.Slot = 0;
            }
        }

        #endregion Methods
    }
}