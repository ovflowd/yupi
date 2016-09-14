using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomErrorMessageComposer : AbstractComposer<int>
    {
        public override void Compose(ISender session, int errorCode)
        {
            // Do nothing by default.
        }
    }
}