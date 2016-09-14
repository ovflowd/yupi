namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class PetInfoMessageComposer : AbstractComposer<PetInfo>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender room, PetInfo pet)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}