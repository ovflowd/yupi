using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Pets
{
    public class PetBreedMessageComposer : Contracts.PetBreedMessageComposer
    {
        public override void Compose(ISender session, uint furniId, PetEntity pet1, PetEntity pet2)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(furniId);
                message.AppendInteger(pet1.Info.Id);
                message.AppendString(pet1.Info.Name);
                message.AppendInteger(pet1.Info.Level);
                message.AppendString(pet1.Info.Look);
                message.AppendString(pet1.Info.Owner.Name);
                message.AppendInteger(pet2.Info.Id);
                message.AppendString(pet2.Info.Name);
                message.AppendInteger(pet2.Info.Level);
                message.AppendString(pet2.Info.Look);
                message.AppendString(pet2.Info.Owner.Name);
                message.AppendInteger(4);

                message.AppendInteger(1);
                /*
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
                */
                throw new NotImplementedException();
            }
        }
    }
}