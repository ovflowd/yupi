using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;
using System.Numerics;

namespace Yupi.Messages.Contracts
{
	public abstract class SetFloorPlanDoorMessageComposer : AbstractComposer<Vector3, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, Vector3 doorPos, int direction)
		{
		 // Do nothing by default.
		}
	}
}
