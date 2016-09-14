namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RoomUserIdleMessageComposer : AbstractComposer<int, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int entityId, bool isAsleep)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}