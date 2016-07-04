using System;
using Yupi.Emulator.Game.Pets;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class AddPetExperienceMessageComposer : AbstractComposer<Pet, int>
	{
		public override void Compose (Yupi.Protocol.ISender session, Pet pet, int amount)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(pet.PetId);
				message.AppendInteger(pet.VirtualId);
				message.AppendInteger(amount);
				session.Send (message);
			}
		}
	}
}

