namespace Yupi.Messages.Contracts
{
    using System.Drawing;

    using Yupi.Protocol.Buffers;

    public abstract class GetFloorPlanUsedCoordsMessageComposer : AbstractComposer<Point[]>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Point[] coords)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}