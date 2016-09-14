using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class FavouriteRoomsUpdateMessageComposer : AbstractComposer<int, bool>
    {
        public override void Compose(Yupi.Protocol.ISender session, int roomId, bool isAdded)
        {
            // Do nothing by default.
        }
    }
}