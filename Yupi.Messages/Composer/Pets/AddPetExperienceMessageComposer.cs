namespace Yupi.Messages.Pets
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class AddPetExperienceMessageComposer : Yupi.Messages.Contracts.AddPetExperienceMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, PetEntity pet, int amount)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(pet.Info.Id);
                message.AppendInteger(pet.Id);
                message.AppendInteger(amount);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}