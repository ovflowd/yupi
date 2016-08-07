using System;
using Yupi.Util;

namespace Yupi.Model.Domain
{
	[Ignore]
	public static class BadgeManager
	{
		public static bool RemoveBadge(this Habbo user, string badeCode) {
			return user.Info.Badges.RemoveAll ((x) => x.Code == badeCode) > 0;
		}

		public static void GiveBadge(this Habbo user, string badeCode) {
			user.Info.Badges.Add (new Badge () {
				Code = badeCode
			});
		}
	}
}

