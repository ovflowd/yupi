using System;

namespace Yupi.Model.Domain
{
	public class BaseInfo
	{
		public virtual int Id { get; protected set; }
		public virtual string Name { get; set; }

		// TODO Do pets have mottos?
		public virtual string Motto { get; set; }
	}
}

