using System;
using Yupi.Emulator.Game.Pets;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class PetInfoMessageComposer : AbstractComposer<Pet>
	{
		public override void Compose (Yupi.Protocol.ISender room, Pet pet)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(pet.PetId);
				message.AppendString(pet.Name);
				message.AppendInteger(pet.Level);
				message.AppendInteger(20);
				message.AppendInteger(pet.Experience);
				message.AppendInteger(pet.ExperienceGoal);
				message.AppendInteger(pet.Energy);
				message.AppendInteger(100);
				message.AppendInteger(pet.Nutrition);
				message.AppendInteger(150);
				message.AppendInteger(pet.Respect);
				message.AppendInteger(pet.OwnerId);
				message.AppendInteger(pet.Age);
				message.AppendString(pet.OwnerName);
				message.AppendInteger(1);
				message.AppendBool(pet.HaveSaddle);
				message.AppendBool(
					Yupi.GetGame()
					.GetRoomManager()
					.GetRoom(pet.RoomId)
					.GetRoomUserManager()
					.GetRoomUserByVirtualId(pet.VirtualId)
					.RidingHorse);
				message.AppendInteger(0);
				message.AppendInteger(pet.AnyoneCanRide);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendString(string.Empty);
				message.AppendBool(false);
				message.AppendInteger(-1);
				message.AppendInteger(-1);
				message.AppendInteger(-1);
				message.AppendBool(false);
				room.Send (message);
			}
		}
	}
}

