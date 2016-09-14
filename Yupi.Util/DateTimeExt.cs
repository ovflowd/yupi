namespace Yupi.Util
{
    using System;

    public static class DateTimeExt
    {
        #region Methods

        public static UnixTimestamp ToUnix(this DateTime dateTime)
        {
            return UnixTimestamp.FromDateTime(dateTime);
        }

        #endregion Methods
    }
}