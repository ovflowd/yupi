using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class WiredEffectMessageComposer : AbstractComposer<IFloorItem, string, int, List<IFloorItem>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IFloorItem item, string extraInfo, int delay, List<IFloorItem> list = null)
		{
		 // Do nothing by default.
		}
	}
}
