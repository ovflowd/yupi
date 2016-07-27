using System;

namespace Yupi.Model.Domain
{
	public class BotInfo
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Motto { get; set; }
		public virtual string Look { get; set; }
		public virtual char Gender { get; set; }
		public virtual Habbo Owner { get; set; }
	}
}

