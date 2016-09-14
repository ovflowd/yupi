namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class FavouriteRoomsUpdateMessageComposer : AbstractComposer<int, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, bool isAdded)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}