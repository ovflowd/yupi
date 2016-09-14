namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class OnGuideSessionInvitedToGuideRoomMessageComposer : AbstractComposer<int, string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, string roomName)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}