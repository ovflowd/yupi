using System;

namespace Yupi.Model.Domain.Components
{
	public class UserBuilderComponent
	{
		public virtual int BuildersExpire { get; set; }
		public virtual int BuildersItemsMax { get; set; }
		public virtual int BuildersItemsUsed { get; set; }
	}
}

