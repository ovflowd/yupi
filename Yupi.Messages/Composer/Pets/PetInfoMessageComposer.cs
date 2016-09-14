using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Pets
{
    public class PetInfoMessageComposer : Yupi.Messages.Contracts.PetInfoMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender room, PetInfo pet)
        {
            /*
            using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
                message.AppendInteger(pet.Id);
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
                message.AppendInteger(pet.Owner.Id);
                message.AppendInteger(pet.Age);
                message.AppendString(pet.Owner.Name);
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
            }*/
            throw new NotImplementedException();
        }
    }
}