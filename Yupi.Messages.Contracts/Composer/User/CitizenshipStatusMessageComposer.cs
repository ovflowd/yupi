using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class CitizenshipStatusMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string value)
        {
            // Do nothing by default.
        }
    }
}