namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SendPerkAllowancesMessageComposer : AbstractComposer<UserInfo, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, UserInfo info, bool enableBetaCamera)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}