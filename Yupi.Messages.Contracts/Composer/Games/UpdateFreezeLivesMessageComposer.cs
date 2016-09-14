using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateFreezeLivesMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int roomId, int lives)
        {
            // Do nothing by default.
        }
    }
}