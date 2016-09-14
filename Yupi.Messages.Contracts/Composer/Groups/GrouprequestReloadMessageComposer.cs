namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GrouprequestReloadMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int groupId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}