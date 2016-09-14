namespace Yupi.Model.Domain
{
    using System;

    public class TradeLock
    {
        #region Properties

        public virtual DateTime ExpiresAt
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        #endregion Properties

        #region Methods

        public virtual bool HasExpired()
        {
            return ExpiresAt.CompareTo(DateTime.Now) <= 0;
        }

        #endregion Methods
    }
}