using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateTileStackMagicHeightComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int itemId, int z)
        {
            // Do nothing by default.
        }
    }
}