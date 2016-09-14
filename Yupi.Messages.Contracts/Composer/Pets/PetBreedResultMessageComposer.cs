namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class PetBreedResultMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int petId, int randomValue)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}