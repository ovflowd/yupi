using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class HotelViewCountdownMessageComposer : AbstractComposer<string>
    {
        public override void Compose(Yupi.Protocol.ISender session, string time)
        {
            // Do nothing by default.
        }
    }
}