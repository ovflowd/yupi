namespace Yupi.Model
{
    using System;

    using Yupi.Model.Domain;

    public class GroupMember
    {
        #region Constructors

        public GroupMember(UserInfo user)
            : this()
        {
            this.User = user;
        }

        protected GroupMember()
        {
            // NHibernate
        }

        #endregion Constructors

        #region Properties

        public virtual int Id
        {
            get; protected set;
        }

        public virtual UserInfo User
        {
            get; protected set;
        }

        #endregion Properties
    }
}