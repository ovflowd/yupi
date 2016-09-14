namespace Yupi.Model.Domain
{
    using System;

    public class FriendRequest
    {
        #region Properties

        public virtual UserInfo From
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual UserInfo To
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public FriendRequest(UserInfo from, UserInfo to)
        {
            From = from;
            To = to;
        }

        protected FriendRequest()
        {
            // For NHIbernate
        }

        #endregion Constructors
    }
}