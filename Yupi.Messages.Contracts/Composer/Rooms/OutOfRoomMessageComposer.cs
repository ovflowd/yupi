namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class OutOfRoomMessageComposer : AbstractComposer<short>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, short code = 0)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}