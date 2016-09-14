using System;
using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.User
{
    public class PetInventoryMessageComposer : Contracts.PetInventoryMessageComposer
    {
        public override void Compose(ISender session, IList<PetItem> pets)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(1);
                message.AppendInteger(pets.Count);
                /*
                foreach (PetInfo pet in pets) {
                    message.AppendInteger(pet.Id);
                    message.AppendString(pet.Name);
                    message.AppendInteger(pet.RaceId);
                    message.AppendInteger(pet.Race);
                    message.AppendString(pet.Color);
                    message.AppendInteger(pet.RaceId);

                    if (pet.Type == "pet_monster" && pet.MoplaBreed != null) {
                        string[] array = pet.MoplaBreed.PlantData.Substring (12).Split (' ');

                        foreach (string s in array)
                            messageBuffer.AppendInteger (int.Parse (s));

                        messageBuffer.AppendInteger (pet.MoplaBreed.GrowingStatus);
                    } else {

                        message.AppendInteger (0);
                        message.AppendInteger (0);
                    }
                }
*/
                throw new NotImplementedException();
                session.Send(message);
            }
        }
    }
}