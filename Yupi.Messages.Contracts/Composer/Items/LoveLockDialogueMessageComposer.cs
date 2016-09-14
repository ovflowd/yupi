using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class LoveLockDialogueMessageComposer : AbstractComposer<ISender, LovelockItem>
    {
        public override void Compose(ISender user1, ISender user2, LovelockItem loveLock)
        {
            // Do nothing by default.
        }
    }
}