using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class DanceStatusMessageComposer : AbstractComposer<int, Dance>
    {
        public override void Compose(ISender room, int entityId, Dance dance)
        {
            // Do nothing by default.
        }
    }
}