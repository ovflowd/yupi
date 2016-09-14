using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	public class EcotronLevel
	{
		public virtual int Id { get; protected set; }
		public virtual IList<EcotronReward> Rewards { get; protected set; }

		public EcotronLevel ()
		{
			Rewards = new List<EcotronReward> ();
		}
	}
}

