using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class SetFloorPlanDoorMessageComposer : AbstractComposer<int, int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int x, int y, int direction)
		{
		 // Do nothing by default.
		}
	}
}
