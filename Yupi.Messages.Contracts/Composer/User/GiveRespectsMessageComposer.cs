using Yupi.Net;
using Yupi.Protocol.Buffers;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GiveRespectsMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(Yupi.Protocol.ISender room, int user, int respect)
        {
            // Do nothing by default.
        }
    }
}