namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UserClubRightsMessageComposer : AbstractComposer<bool, int, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool hasVIP, int rank, bool isAmbadassor = false)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}