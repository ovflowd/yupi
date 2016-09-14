using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class HotelViewCountdownMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string time)
        {
            // Do nothing by default.
        }
    }
}