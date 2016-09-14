namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class DoorbellMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string username)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}