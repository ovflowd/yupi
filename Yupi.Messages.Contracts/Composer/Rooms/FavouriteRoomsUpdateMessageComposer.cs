using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class FavouriteRoomsUpdateMessageComposer : AbstractComposer<int, bool>
    {
        public override void Compose(ISender session, int roomId, bool isAdded)
        {
            // Do nothing by default.
        }
    }
}