using System;

namespace Yupi.Model.Domain
{
	public class TalentLevel
	{
		public virtual int Id { get; protected set; }

		public virtual int Level { get; set; }

		public virtual BaseItem PrizeItem { get; set; }
		public virtual Achievement Achievement{ get; set; }
	}
}

