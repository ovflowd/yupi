using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using System.Globalization;
using Yupi.Model.Domain;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
	public abstract class LoadInventoryMessageComposer : AbstractComposer<Inventory>
	{
		public override void Compose(Yupi.Protocol.ISender session, Inventory inventory)
		{
		 // Do nothing by default.
		}
	}
}
