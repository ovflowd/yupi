namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RoomRatingMessageComposer : AbstractComposer<int, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int rating, bool canVote)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}