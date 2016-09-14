using System;
using System.Collections.Generic;
using System.Linq;
using Yupi.Util;

namespace Yupi.Model.Domain.Components
{
    public class RelationshipComponent
    {
        [OneToMany]
        public virtual IList<Relationship> Relationships { get; protected set; }

        // TODO Not really happy with this implementation
        public virtual IList<FriendRequest> SentRequests { get; protected set; }
        public virtual IList<FriendRequest> ReceivedRequests { get; protected set; }

        public RelationshipComponent()
        {
            Relationships = new List<Relationship>();
            SentRequests = new List<FriendRequest>();
            ReceivedRequests = new List<FriendRequest>();
        }

        public virtual bool IsFriendsWith(UserInfo user)
        {
            return Relationships.Any(x => x.Friend == user);
        }

        public virtual Relationship FindByUser(UserInfo user)
        {
            return Relationships.Single(x => x.Friend == user);
        }

        public virtual Relationship FindByUser(int userId)
        {
            return Relationships.SingleOrDefault(x => x.Friend.Id == userId);
        }

        public virtual Relationship Add(UserInfo friend)
        {
            var relationship = new Relationship()
            {
                Friend = friend,
                Type = 0 // TODO ???
            };

            Relationships.Add(relationship);
            return relationship;
        }

        public virtual bool HasSentRequestTo(UserInfo user)
        {
            return SentRequests.Any(x => x.To == user);
        }
    }
}