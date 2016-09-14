using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class InternalLinkMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string link)
        {
            // Do nothing by default.
        }
    }
}