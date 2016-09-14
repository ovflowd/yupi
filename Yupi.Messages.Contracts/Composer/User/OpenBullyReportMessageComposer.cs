namespace Yupi.Messages.Contracts
{
    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public abstract class OpenBullyReportMessageComposer : AbstractComposerVoid
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}