namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UserLeftRoomMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int virtualId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}