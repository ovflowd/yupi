namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class SendCampaignBadgeMessageComposer : AbstractComposer<string, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string badgeName, bool hasBadge)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}