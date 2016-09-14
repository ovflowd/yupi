using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UpdateFreezeLivesMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender session, int roomId, int lives)
        {
            // Do nothing by default.
        }
    }
}