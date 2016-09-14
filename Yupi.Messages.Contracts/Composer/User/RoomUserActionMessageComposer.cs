namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class RoomUserActionMessageComposer : AbstractComposer<int, UserAction>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, int virtualId, UserAction action)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}