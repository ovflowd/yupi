using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class FloorMapMessageComposer : AbstractComposer<string, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, string heightmap, int wallHeight)
        {
            // Do nothing by default.
        }
    }
}