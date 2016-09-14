using System;
using System.Net;

namespace Yupi.Model.Domain
{
	public class UserBan
	{
		public virtual int Id { get; protected set; }
		public virtual UserInfo User { get; set; }
		public virtual IPAddress IP { get; set; }
		public virtual string MachineId { get; set; }
		public virtual DateTime ExpiresAt { get; set; }
		public virtual string Reason { get; set; }
	}
}

