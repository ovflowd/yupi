using System;
using System.Collections.Generic;
using Yupi.Util;
using System.Linq;

namespace Yupi.Model.Domain.Components
{
	public class UserBadgeComponent
	{
		[OneToMany]
		public virtual IList<Badge> Badges { get; protected set; }

		public virtual bool RemoveBadge(string badeCode) {
			return Badges.RemoveAll ((x) => x.Code == badeCode) > 0;
		}

		public virtual void GiveBadge(string badeCode) {
			Badges.Add (new Badge () {
				Code = badeCode
			});
		}

		public virtual IList<Badge> GetVisible() {
			return Badges.Where (x => x.Slot > 0).ToList ();
		}
	}
}

