namespace Yupi.Messages.Contracts
{
    using System.Numerics;

    using Yupi.Model.Domain.Components;
    using Yupi.Protocol.Buffers;

    public abstract class SetFloorPlanDoorMessageComposer : AbstractComposer<Vector3, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Vector3 doorPos, int direction)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}