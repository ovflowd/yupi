using System;

namespace Yupi.Util
{
	public static class DateTimeExt
	{
		public static UnixTimestamp ToUnix(this DateTime dateTime) {
			return UnixTimestamp.FromDateTime(dateTime);
		}
	}
}

