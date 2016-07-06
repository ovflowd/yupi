using System.Collections.Generic;
using System.Data;
using FluentNHibernate.Data;
using System;
using Yupi.Model.Users;

namespace Yupi.Model.Messenger
{
    public class Message : Entity
    {
		public virtual Habbo From { get; set; }
		public virtual string Text { get; set; }
		public virtual DateTime Timestamp { get; set; }
	}
}