using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SellablePetBreedsMessageComposer : AbstractComposer<string>
    {
        public override void Compose(ISender session, string type)
        {
            // Do nothing by default.
        }
    }
}