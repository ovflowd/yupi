using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;



namespace Yupi.Messages.Pets
{
	public class PetTrainerPanelMessageComposer : AbstractComposer<uint>
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint petId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (petId);

				Dictionary<uint, PetCommand> totalPetCommands = PetCommandHandler.GetAllPetCommands();
				Dictionary<uint, PetCommand> petCommands = PetCommandHandler.GetPetCommandByPetType(petData.Type);

				message.AppendInteger(totalPetCommands.Count);

				foreach (uint sh in totalPetCommands.Keys)
					message.AppendInteger(sh);

				message.AppendInteger(petCommands.Count);

				foreach (uint sh in petCommands.Keys)
					message.AppendInteger(sh);
				
				session.Send (message);
			}
		}
	}
}

