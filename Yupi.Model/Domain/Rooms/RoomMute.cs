namespace Yupi.Model.Domain
{
    using System;

    public class RoomMute
    {
        #region Properties

        public virtual UserInfo Entity
        {
            get; set;
        }

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

        // TODO Clean up mutes from time to time
        public virtual bool HasExpired()
        {
            return DateTime.Now > ExpiresAt;
        }

        #endregion Methods
    }
}