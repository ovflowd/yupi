namespace Yupi.Model.Domain
{
    using System;

    // TODO HC & VIP
    public class Subscription
    {
        #region Properties

        public virtual DateTime ActivateTime
        {
            get; set;
        }

        public virtual DateTime ExpireTime
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual DateTime LastGiftTime
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public Subscription()
        {
            ExpireTime = DateTime.Now;
            ActivateTime = DateTime.Now;
            LastGiftTime = DateTime.Now;
        }

        #endregion Constructors

        #region Methods

        public virtual bool IsValid()
        {
            return ExpireTime > DateTime.Now;
        }

        #endregion Methods
    }
}