namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class BroadcastNotifMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string text)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}