using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class RoomErrorMessageComposer : AbstractComposer<int>
    {
        public override void Compose(Yupi.Protocol.ISender session, int errorCode)
        {
            // Do nothing by default.
        }
    }
}