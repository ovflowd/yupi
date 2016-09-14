using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
    public abstract class DanceStatusMessageComposer : AbstractComposer<int, Dance>
    {
        public override void Compose(Yupi.Protocol.ISender room, int entityId, Dance dance)
        {
            // Do nothing by default.
        }
    }
}