using System;

namespace Yupi.Model.Domain
{
	public class ChatlogEntry
	{
		public virtual int Id { get; set; }
		public virtual UserInfo User { get; set; }
		public virtual DateTime Timestamp { get; set; }
		public virtual string Message { get; set; }

		// TODO Global?
		public virtual bool GlobalMessage { get; set; }
	}
}

