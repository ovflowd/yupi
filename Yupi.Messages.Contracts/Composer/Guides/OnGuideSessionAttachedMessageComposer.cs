using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OnGuideSessionAttachedMessageComposer : AbstractComposer<bool, int, string, int>
    {
        public override void Compose(ISender session, bool val1, int userId, string message, int val2)
        {
            // Do nothing by default.
        }
    }
}