using System;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public class PetInventoryMessageComposer : AbstractComposer<IList<PetInfo>>
	{
		public override void Compose (Yupi.Protocol.ISender session, IList<PetInfo> pets)
		{
			
		}
	}
}

