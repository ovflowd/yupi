using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SetCameraPriceMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int credits, int seasonalCurrency)
        {
            // Do nothing by default.
        }
    }
}