namespace Yupi.Model.Domain
{
    using System;
    using System.Net;

    public class UserBan
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

        public virtual IPAddress IP
        {
            get; set;
        }

        public virtual string MachineId
        {
            get; set;
        }

        public virtual string Reason
        {
            get; set;
        }

        public virtual UserInfo User
        {
            get; set;
        }

        #endregion Properties
    }
}