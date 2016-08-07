using System.Drawing;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class GetFloorPlanUsedCoordsMessageComposer : AbstractComposer<Point[]>
	{
		public override void Compose(Yupi.Protocol.ISender session, Point[] coords)
		{
		 // Do nothing by default.
		}
	}
}
