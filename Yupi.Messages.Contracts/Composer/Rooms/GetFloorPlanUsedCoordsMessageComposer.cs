using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GetFloorPlanUsedCoordsMessageComposer : AbstractComposer<Point[]>
    {
        public override void Compose(ISender session, Point[] coords)
        {
            // Do nothing by default.
        }
    }
}