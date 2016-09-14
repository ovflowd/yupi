using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionMsgMessageComposer : AbstractComposer<string, int>
    {
        public override void Compose(ISender session, string content, int userId)
        {
            // Do nothing by default.
        }
    }
}