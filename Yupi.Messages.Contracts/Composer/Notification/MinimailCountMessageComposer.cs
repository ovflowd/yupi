using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class MinimailCountMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int count)
        {
            // Do nothing by default.
        }
    }
}