using Yupi.Protocol.Buffers;
using Yupi.Net;

namespace Yupi.Messages.Contracts
{
    public abstract class BullyReportSentMessageComposer : AbstractComposerVoid
    {
        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }
    }
}