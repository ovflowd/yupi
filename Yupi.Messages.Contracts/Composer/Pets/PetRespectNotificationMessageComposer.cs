namespace Yupi.Messages.Contracts
{
    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public abstract class PetRespectNotificationMessageComposer : AbstractComposer<PetEntity>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, PetEntity pet)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}