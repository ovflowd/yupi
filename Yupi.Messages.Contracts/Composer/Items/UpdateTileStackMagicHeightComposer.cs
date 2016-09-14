using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateTileStackMagicHeightComposer : AbstractComposer<int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int itemId, int z)
        {
            // Do nothing by default.
        }
    }
}