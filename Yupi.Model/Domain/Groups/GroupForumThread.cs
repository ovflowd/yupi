namespace Yupi.Model.Domain
{
    using System;
    using System.Collections.Generic;

    public class GroupForumThread
    {
        #region Properties

        public virtual DateTime CreatedAt
        {
            get; set;
        }

        public virtual UserInfo Creator
        {
            get; set;
        }

        public virtual bool Hidden
        {
            get; set;
        }

        public virtual UserInfo HiddenBy
        {
            get; set;
        }

        public virtual int Id
        {
            get; protected set;
        }

        public virtual bool Locked
        {
            get; set;
        }

        public virtual bool Pinned
        {
            get; set;
        }

        public virtual IList<GroupForumPost> Posts
        {
            get; protected set;
        }

        public virtual string Subject
        {
            get; set;
        }

        #endregion Properties

        #region Constructors

        public GroupForumThread()
        {
            Posts = new List<GroupForumPost>();
            CreatedAt = DateTime.Now;
        }

        #endregion Constructors
    }
}