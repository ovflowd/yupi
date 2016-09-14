using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RecyclingStateMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int insertId)
        {
            // Do nothing by default.
        }
    }
}