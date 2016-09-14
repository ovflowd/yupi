namespace Yupi.Messages.Pets
{
    using System;
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public class PetTrainerPanelMessageComposer : Yupi.Messages.Contracts.PetTrainerPanelMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int petId)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
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

        #endregion Methods
    }
}