namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GroupRoomMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, int groupId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}