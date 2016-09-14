namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class AddPetExperienceMessageComposer : AbstractComposer<PetEntity, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, PetEntity pet, int amount)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}