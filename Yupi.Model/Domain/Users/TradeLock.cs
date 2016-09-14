using System;

namespace Yupi.Model.Domain
{
    public class TradeLock
    {
        public virtual int Id { get; protected set; }

        public virtual DateTime ExpiresAt { get; set; }

        public virtual bool HasExpired()
        {
            return ExpiresAt.CompareTo(DateTime.Now) <= 0;
        }
    }
}