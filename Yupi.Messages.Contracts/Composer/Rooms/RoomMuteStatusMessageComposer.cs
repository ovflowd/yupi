namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RoomMuteStatusMessageComposer : AbstractComposer<bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool isMuted)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}