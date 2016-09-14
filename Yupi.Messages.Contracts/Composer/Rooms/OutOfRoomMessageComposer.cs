using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class OutOfRoomMessageComposer : AbstractComposer<short>
    {
        public override void Compose(ISender session, short code = 0)
        {
            // Do nothing by default.
        }
    }
}