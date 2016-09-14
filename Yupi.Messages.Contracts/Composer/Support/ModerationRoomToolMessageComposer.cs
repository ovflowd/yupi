namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class ModerationRoomToolMessageComposer : AbstractComposer<RoomData, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData data, bool isLoaded)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}