using System;
using Yupi.Emulator.Game.Pets.Enums;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class PetInformationComposer
    {
        internal static SimpleServerMessageBuffer GenerateMessage(Pet pet)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("PetInfoMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(pet.PetId);
            simpleServerMessageBuffer.AppendString(pet.Name);

            if (pet.Type == "pet_monster")
            {
                simpleServerMessageBuffer.AppendInteger(pet.MoplaBreed.GrowingStatus);
                simpleServerMessageBuffer.AppendInteger(7);
            }
            else
            {
                simpleServerMessageBuffer.AppendInteger(pet.Level);
                simpleServerMessageBuffer.AppendInteger(Pet.MaxLevel);
            }

            simpleServerMessageBuffer.AppendInteger(pet.Experience);
            simpleServerMessageBuffer.AppendInteger(pet.ExperienceGoal);
            simpleServerMessageBuffer.AppendInteger(pet.Energy);
            simpleServerMessageBuffer.AppendInteger(Pet.MaxEnergy);
            simpleServerMessageBuffer.AppendInteger(pet.Nutrition);
            simpleServerMessageBuffer.AppendInteger(Pet.MaxNutrition);
            simpleServerMessageBuffer.AppendInteger(pet.Respect);
            simpleServerMessageBuffer.AppendInteger(pet.OwnerId);
            simpleServerMessageBuffer.AppendInteger(pet.Age);
            simpleServerMessageBuffer.AppendString(pet.OwnerName);
            simpleServerMessageBuffer.AppendInteger(pet.Type == "pet_monster" ? 0 : 1);
            simpleServerMessageBuffer.AppendBool(pet.HaveSaddle);
            simpleServerMessageBuffer.AppendBool(false);
            simpleServerMessageBuffer.AppendInteger(0);
            simpleServerMessageBuffer.AppendInteger(pet.AnyoneCanRide);

            if (pet.Type == "pet_monster")
                simpleServerMessageBuffer.AppendBool(pet.MoplaBreed.LiveState == MoplaState.Grown);
            else
                simpleServerMessageBuffer.AppendBool(false);

            simpleServerMessageBuffer.AppendBool(false);

            if (pet.Type == "pet_monster")
                simpleServerMessageBuffer.AppendBool(pet.MoplaBreed.LiveState == MoplaState.Dead);
            else
                simpleServerMessageBuffer.AppendBool(false);

            simpleServerMessageBuffer.AppendInteger(pet.Rarity);

            if (pet.Type == "pet_monster")
            {
                simpleServerMessageBuffer.AppendInteger(129600);
                int lastHealthSeconds = (int) (pet.LastHealth - DateTime.Now).TotalSeconds;
                int untilGrownSeconds = (int) (pet.UntilGrown - DateTime.Now).TotalSeconds;

                if (lastHealthSeconds < 0)
                    lastHealthSeconds = 0;

                if (untilGrownSeconds < 0)
                    untilGrownSeconds = 0;

                switch (pet.MoplaBreed.LiveState)
                {
                    case MoplaState.Dead:
                        simpleServerMessageBuffer.AppendInteger(0);
                        simpleServerMessageBuffer.AppendInteger(0);
                        break;

                    case MoplaState.Grown:
                        simpleServerMessageBuffer.AppendInteger(lastHealthSeconds);
                        simpleServerMessageBuffer.AppendInteger(0);
                        break;

                    default:
                        simpleServerMessageBuffer.AppendInteger(lastHealthSeconds);
                        simpleServerMessageBuffer.AppendInteger(untilGrownSeconds);
                        break;
                }
            }
            else
            {
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
                simpleServerMessageBuffer.AppendInteger(-1);
            }

            simpleServerMessageBuffer.AppendBool(true);

            return simpleServerMessageBuffer;
        }
    }
}