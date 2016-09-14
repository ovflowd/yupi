namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RoomUnbanUserMessageComposer : AbstractComposer<uint, uint>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint roomId, uint userId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}