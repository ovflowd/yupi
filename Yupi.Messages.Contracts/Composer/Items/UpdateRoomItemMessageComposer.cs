namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class UpdateRoomItemMessageComposer : AbstractComposer<FloorItem>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}