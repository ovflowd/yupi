using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FloorMapMessageComposer : AbstractComposer<string, int>
    {
        public override void Compose(ISender session, string heightmap, int wallHeight)
        {
            // Do nothing by default.
        }
    }
}