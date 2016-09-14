using System;
using Yupi.Model.Domain;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
    public class PetRespectNotificationMessageComposer : Yupi.Messages.Contracts.PetRespectNotificationMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, PetEntity pet)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(pet.Id);
                message.AppendInteger(pet.Info.Id);
                message.AppendString(pet.Info.Name);

                message.AppendInteger(pet.Info.RaceId);
                message.AppendInteger(pet.Info.Race);

                /*
                message.AppendString(pet.Type == "pet_monster" ? "ffffff" : pet.Info.Color);
                message.AppendInteger(pet.Type == "pet_monster" ? 0u : pet.Info.RaceId);

                if (pet.Type == "pet_monster" && pet.MoplaBreed != null)
                {
                    string[] array = pet.MoplaBreed.PlantData.Substring(12).Split(' ');
                    string[] array2 = array;

                    foreach (string s in array2)
                        message.AppendInteger(int.Parse(s));

                    message.AppendInteger(pet.MoplaBreed.GrowingStatus);

                    return;
                }*/
                throw new NotImplementedException();

                message.AppendInteger(0);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}