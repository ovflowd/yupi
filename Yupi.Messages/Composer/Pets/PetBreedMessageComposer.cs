using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class PetBreedMessageComposer : AbstractComposer<uint, Pet, Pet>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint furniId, Pet pet1, Pet pet2)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(furniId);
				message.AppendInteger(pet1.PetId);
				message.AppendString(pet1.Name);
				message.AppendInteger(pet1.Level);
				message.AppendString(pet1.Look);
				message.AppendString(pet1.OwnerName);
				message.AppendInteger(pet2.PetId);
				message.AppendString(pet2.Name);
				message.AppendInteger(pet2.Level);
				message.AppendString(pet2.Look);
				message.AppendString(pet2.OwnerName);
				message.AppendInteger(4);

				message.AppendInteger(1);

				switch (pet1.Type)
				{
				case "pet_terrier":
					message.AppendInteger(PetBreeding.TerrierEpicRace.Length);

					foreach (int value in PetBreeding.TerrierEpicRace)
						message.AppendInteger(value);

					break;

				case "pet_bear":
					message.AppendInteger(PetBreeding.BearEpicRace.Length);

					foreach (int value in PetBreeding.BearEpicRace)
						message.AppendInteger(value);

					break;
				}

				message.AppendInteger(2);

				switch (pet1.Type)
				{
				case "pet_terrier":
					message.AppendInteger(PetBreeding.TerrierRareRace.Length);

					foreach (int value in PetBreeding.TerrierRareRace)
						message.AppendInteger(value);

					break;

				case "pet_bear":
					message.AppendInteger(PetBreeding.BearRareRace.Length);

					foreach (int value in PetBreeding.BearRareRace)
						message.AppendInteger(value);

					break;
				}

				message.AppendInteger(3);

				switch (pet1.Type)
				{
				case "pet_terrier":
					message.AppendInteger(PetBreeding.TerrierNoRareRace.Length);

					foreach (int value in PetBreeding.TerrierNoRareRace)
						message.AppendInteger(value);

					break;

				case "pet_bear":
					message.AppendInteger(PetBreeding.BearNoRareRace.Length);

					foreach (int value in PetBreeding.BearNoRareRace)
						message.AppendInteger(value);

					break;
				}

				message.AppendInteger(94);

				switch (pet1.Type)
				{
				case "pet_terrier":
					message.AppendInteger(PetBreeding.TerrierNormalRace.Length);

					foreach (int value in PetBreeding.TerrierNormalRace)
						message.AppendInteger(value);

					break;

				case "pet_bear":
					message.AppendInteger(PetBreeding.BearNormalRace.Length);

					foreach (int value in PetBreeding.BearNormalRace)
						message.AppendInteger(value);

					break;
				}

				message.AppendInteger(pet1.Type == "pet_terrier"
					? PetTypeManager.GetPetRaceIdByType("pet_terrierbaby")
					: PetTypeManager.GetPetRaceIdByType("pet_bearbaby"));
				session.Send (message);
			}
		}
	}
}

