namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GroupDeletedMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int groupId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}