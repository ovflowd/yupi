namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class SerializePetMessageComposer : AbstractComposer<PetEntity>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, PetEntity pet)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}