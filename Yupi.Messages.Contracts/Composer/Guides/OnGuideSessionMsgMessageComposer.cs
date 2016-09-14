using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionMsgMessageComposer : AbstractComposer<string, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, string content, int userId)
        {
            // Do nothing by default.
        }
    }
}