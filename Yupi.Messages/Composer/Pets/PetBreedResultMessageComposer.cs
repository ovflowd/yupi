namespace Yupi.Messages.Pets
{
    using System;

    using Yupi.Protocol.Buffers;

    public class PetBreedResultMessageComposer : Yupi.Messages.Contracts.PetBreedResultMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int petId, int randomValue)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(petId);
                message.AppendInteger(randomValue);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}