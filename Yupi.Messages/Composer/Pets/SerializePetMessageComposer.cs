using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class SerializePetMessageComposer : AbstractComposer<RoomUser>
	{
		public override void Compose (Yupi.Protocol.ISender room, RoomUser pet)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(pet.PetData.VirtualId);
				message.AppendInteger(pet.PetData.PetId);
				message.AppendInteger(pet.PetData.RaceId);
				message.AppendInteger(pet.PetData.Race);
				message.AppendString(pet.PetData.Color.ToLower());
				if (pet.PetData.HaveSaddle)
				{
					message.AppendInteger(2);
					message.AppendInteger(3);
					message.AppendInteger(4);
					message.AppendInteger(9);
					message.AppendInteger(0);
					message.AppendInteger(3);
					message.AppendInteger(pet.PetData.PetHair);
					message.AppendInteger(pet.PetData.HairDye);
					message.AppendInteger(3);
					message.AppendInteger(pet.PetData.PetHair);
					message.AppendInteger(pet.PetData.HairDye);
				}
				else
				{
					message.AppendInteger(1);
					message.AppendInteger(2);
					message.AppendInteger(2);
					message.AppendInteger(pet.PetData.PetHair);
					message.AppendInteger(pet.PetData.HairDye);
					message.AppendInteger(3);
					message.AppendInteger(pet.PetData.PetHair);
					message.AppendInteger(pet.PetData.HairDye);
				}
				message.AppendBool(pet.PetData.HaveSaddle);
				message.AppendBool(pet.RidingHorse);
				room.Send (message);
			}
		}
	}
}

