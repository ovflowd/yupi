using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class GeneralErrorHabboMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int errorId)
        {
            // Do nothing by default.
        }
    }
}