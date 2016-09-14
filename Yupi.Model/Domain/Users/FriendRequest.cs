using System;

namespace Yupi.Model.Domain
{
    public class FriendRequest
    {
        public virtual int Id { get; protected set; }
        public virtual UserInfo To { get; set; }
        public virtual UserInfo From { get; set; }

        protected FriendRequest()
        {
            // For NHIbernate
        }

        public FriendRequest(UserInfo from, UserInfo to)
        {
            From = from;
            To = to;
        }
    }
}