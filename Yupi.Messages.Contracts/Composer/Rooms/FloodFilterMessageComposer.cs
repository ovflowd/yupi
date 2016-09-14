using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FloodFilterMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int remainingSeconds)
        {
            // Do nothing by default.
        }
    }
}