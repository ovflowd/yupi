using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class SetCameraPriceMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int credits, int seasonalCurrency)
        {
            // Do nothing by default.
        }
    }
}