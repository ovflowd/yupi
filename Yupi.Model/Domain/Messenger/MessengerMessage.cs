using System.Collections.Generic;
using System.Data;
using System;
using Yupi.Model.Domain.Users;

namespace Yupi.Model.Domain.Messenger
{
    public class MessengerMessage
    {
		public virtual int Id { get; set; }
		public virtual Habbo From { get; set; }
		public virtual string Text { get; set; }
		public virtual DateTime Timestamp { get; set; }
	}
}