using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LandingWidgetMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string text)
        {
            // Do nothing by default.
        }
    }
}