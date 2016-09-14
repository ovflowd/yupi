using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class UserLeftRoomMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int virtualId)
        {
            // Do nothing by default.
        }
    }
}