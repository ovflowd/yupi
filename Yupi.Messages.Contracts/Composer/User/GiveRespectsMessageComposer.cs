using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class GiveRespectsMessageComposer : AbstractComposer<int, int>
    {
        public override void Compose(ISender room, int user, int respect)
        {
            // Do nothing by default.
        }
    }
}