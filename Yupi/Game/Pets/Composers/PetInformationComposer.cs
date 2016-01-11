using System;
using Yupi.Game.Pets.Enums;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets.Composers
{
    internal class PetInformationComposer
    {
        internal static ServerMessage GenerateMessage(Pet pet)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetInfoMessageComposer"));

            serverMessage.AppendInteger(pet.PetId);
            serverMessage.AppendString(pet.Name);

            if (pet.Type == "pet_monster")
            {
                serverMessage.AppendInteger(pet.MoplaBreed.GrowingStatus);
                serverMessage.AppendInteger(7);
            }
            else
            {
                serverMessage.AppendInteger(pet.Level);
                serverMessage.AppendInteger(Pet.MaxLevel);
            }

            serverMessage.AppendInteger(pet.Experience);
            serverMessage.AppendInteger(pet.ExperienceGoal);
            serverMessage.AppendInteger(pet.Energy);
            serverMessage.AppendInteger(Pet.MaxEnergy);
            serverMessage.AppendInteger(pet.Nutrition);
            serverMessage.AppendInteger(Pet.MaxNutrition);
            serverMessage.AppendInteger(pet.Respect);
            serverMessage.AppendInteger(pet.OwnerId);
            serverMessage.AppendInteger(pet.Age);
            serverMessage.AppendString(pet.OwnerName);
            serverMessage.AppendInteger(pet.Type == "pet_monster" ? 0 : 1);
            serverMessage.AppendBool(pet.HaveSaddle);
            serverMessage.AppendBool(false);
            serverMessage.AppendInteger(0);
            serverMessage.AppendInteger(pet.AnyoneCanRide);

            if (pet.Type == "pet_monster")
                serverMessage.AppendBool(pet.MoplaBreed.LiveState == MoplaState.Grown);
            else
                serverMessage.AppendBool(false);

            serverMessage.AppendBool(false);

            if (pet.Type == "pet_monster")
                serverMessage.AppendBool(pet.MoplaBreed.LiveState == MoplaState.Dead);
            else
                serverMessage.AppendBool(false);

            serverMessage.AppendInteger(pet.Rarity);

            if (pet.Type == "pet_monster")
            {
                serverMessage.AppendInteger(129600);
                int lastHealthSeconds = (int) (pet.LastHealth - DateTime.Now).TotalSeconds;
                int untilGrownSeconds = (int) (pet.UntilGrown - DateTime.Now).TotalSeconds;

                if (lastHealthSeconds < 0)
                    lastHealthSeconds = 0;

                if (untilGrownSeconds < 0)
                    untilGrownSeconds = 0;

                switch (pet.MoplaBreed.LiveState)
                {
                    case MoplaState.Dead:
                        serverMessage.AppendInteger(0);
                        serverMessage.AppendInteger(0);
                        break;

                    case MoplaState.Grown:
                        serverMessage.AppendInteger(lastHealthSeconds);
                        serverMessage.AppendInteger(0);
                        break;

                    default:
                        serverMessage.AppendInteger(lastHealthSeconds);
                        serverMessage.AppendInteger(untilGrownSeconds);
                        break;
                }
            }
            else
            {
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
                serverMessage.AppendInteger(-1);
            }

            serverMessage.AppendBool(true);

            return serverMessage;
        }
    }
}