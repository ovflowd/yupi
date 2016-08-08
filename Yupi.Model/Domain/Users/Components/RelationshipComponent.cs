using System;
using System.Collections.Generic;
using System.Linq;

namespace Yupi.Model.Domain.Components
{
	public class RelationshipComponent
	{
		[OneToMany]
		public virtual IList<Relationship> Relationships { get; protected set; }

		public virtual IList<UserInfo> SentRequests { get; protected set; }

		public virtual bool IsFriendsWith(UserInfo user) {
			return Relationships.Any (x => x.Friend == user);
		}

		public virtual Relationship FindByUser(UserInfo user) {
			return Relationships.Single (x => x.Friend == user);
		}
	}
}

