using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Pets
{
    public class AddPetExperienceMessageComposer : Yupi.Messages.Contracts.AddPetExperienceMessageComposer
    {
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
    }
}