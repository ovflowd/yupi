using System;
using Yupi.Protocol;

namespace Yupi.Messages.Pets
{
    public class PetTrainerPanelMessageComposer : Contracts.PetTrainerPanelMessageComposer
    {
        public override void Compose(ISender session, int petId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(petId);
                /*
                Dictionary<uint, PetCommand> totalPetCommands = PetCommandHandler.GetAllPetCommands();
                Dictionary<uint, PetCommand> petCommands = PetCommandHandler.GetPetCommandByPetType(petData.Type);

                message.AppendInteger(totalPetCommands.Count);

                foreach (uint sh in totalPetCommands.Keys)
                    message.AppendInteger(sh);

                message.AppendInteger(petCommands.Count);

                foreach (uint sh in petCommands.Keys)
                    message.AppendInteger(sh);
                
                session.Send (message);*/
                throw new NotImplementedException();
            }
        }
    }
}