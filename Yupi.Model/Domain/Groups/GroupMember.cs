using System;
using Yupi.Model.Domain;

namespace Yupi.Model
{
	public class GroupMember
	{
		public virtual int Id { get; protected set; }
		public virtual UserInfo User { get; protected set; }

		public GroupMember (UserInfo user) : this()
		{
			this.User = user;
		}

		protected GroupMember ()
		{
			// NHibernate
		}
		
	}
}

