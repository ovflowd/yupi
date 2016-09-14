namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RemovePetFromInventoryComposer : AbstractComposer<uint>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint petId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}