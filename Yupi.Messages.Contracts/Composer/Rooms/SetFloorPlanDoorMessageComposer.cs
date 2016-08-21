using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Contracts
{
	public abstract class SetFloorPlanDoorMessageComposer : AbstractComposer<Vector3D, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, Vector3D doorPos, int direction)
		{
		 // Do nothing by default.
		}
	}
}
