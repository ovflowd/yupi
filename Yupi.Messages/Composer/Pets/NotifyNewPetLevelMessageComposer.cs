using System;
using Yupi.Model.Domain;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class NotifyNewPetLevelMessageComposer : Yupi.Messages.Contracts.NotifyNewPetLevelMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, PetInfo pet)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (pet.Id);
				message.AppendString (pet.Name);
				message.AppendInteger (pet.Level);
				message.AppendInteger (pet.RaceId);
				message.AppendInteger (pet.Race);
				message.AppendString (pet.Type == "pet_monster" ? "ffffff" : pet.Color);
				message.AppendInteger (pet.Type == "pet_monster" ? 0u : pet.RaceId);

				if (pet.Type == "pet_monster" && pet.MoplaBreed != null) {
					string[] array = pet.MoplaBreed.PlantData.Substring (12).Split (' ');
					string[] array2 = array;

					foreach (string s in array2)
						message.AppendInteger (int.Parse (s));

					message.AppendInteger (pet.MoplaBreed.GrowingStatus);

					return;
				}

				message.AppendInteger (0);
				message.AppendInteger (0);
				session.Send (message);
			}
		}
	}
}

