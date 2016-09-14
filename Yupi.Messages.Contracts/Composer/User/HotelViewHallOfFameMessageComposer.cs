using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class HotelViewHallOfFameMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string code)
        {
            // Do nothing by default.
        }
    }
}