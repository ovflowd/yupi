using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
	public abstract class SetFloorPlanDoorMessageComposer : AbstractComposer<Vector, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, Vector doorPos, int direction)
		{
		 // Do nothing by default.
		}
	}
}
