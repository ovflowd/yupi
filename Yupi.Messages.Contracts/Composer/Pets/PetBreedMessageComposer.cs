namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class PetBreedMessageComposer : AbstractComposer<uint, PetEntity, PetEntity>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint furniId, PetEntity pet1, PetEntity pet2)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}