namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RoomEnterErrorMessageComposer : AbstractComposer<RoomEnterErrorMessageComposer.Error>
    {
        #region Enumerations

        public enum Error
        {
            ROOM_FULL = 1,
            BANNED = 4 // TODO Verify
        }

        #endregion Enumerations

        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Error error)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}