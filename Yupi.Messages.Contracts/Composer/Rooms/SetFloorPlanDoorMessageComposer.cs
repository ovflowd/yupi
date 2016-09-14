using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SetFloorPlanDoorMessageComposer : AbstractComposer<Vector3, int>
    {
        public override void Compose(ISender session, Vector3 doorPos, int direction)
        {
            // Do nothing by default.
        }
    }
}