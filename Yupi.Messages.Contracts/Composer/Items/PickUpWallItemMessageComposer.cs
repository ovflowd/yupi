using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class PickUpWallItemMessageComposer : AbstractComposer<IWallItem, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, IWallItem item, int pickerId)
		{
		 // Do nothing by default.
		}
	}
}
