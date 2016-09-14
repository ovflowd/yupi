namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class NotifyNewPetLevelMessageComposer : AbstractComposer<PetInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, PetInfo pet)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}