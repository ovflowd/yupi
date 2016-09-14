namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GroupAreYouSureMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int userId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}