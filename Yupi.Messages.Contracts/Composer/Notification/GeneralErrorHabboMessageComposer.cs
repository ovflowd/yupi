using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GeneralErrorHabboMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int errorId)
        {
            // Do nothing by default.
        }
    }
}