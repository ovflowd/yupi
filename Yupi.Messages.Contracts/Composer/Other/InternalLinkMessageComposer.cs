namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class InternalLinkMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string link)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}