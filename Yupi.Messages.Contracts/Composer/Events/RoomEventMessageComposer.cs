namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomEventMessageComposer : AbstractComposer<RoomData, RoomEvent>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, RoomData room, RoomEvent roomEvent)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}