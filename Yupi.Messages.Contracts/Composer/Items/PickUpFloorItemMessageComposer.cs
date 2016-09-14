namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class PickUpFloorItemMessageComposer : AbstractComposer<FloorItem, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FloorItem item, int pickerId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}