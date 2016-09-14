using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class DoorbellMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string username)
        {
            // Do nothing by default.
        }
    }
}