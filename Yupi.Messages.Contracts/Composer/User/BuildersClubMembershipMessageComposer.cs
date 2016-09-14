namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class BuildersClubMembershipMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int expire, int maxItems)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}