namespace Yupi.Messages.Contracts
{
    using Yupi.Net;
    using Yupi.Protocol.Buffers;

    public abstract class CitizenshipStatusMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string value)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}