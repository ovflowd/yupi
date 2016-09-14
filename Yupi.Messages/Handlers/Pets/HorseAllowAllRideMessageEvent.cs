using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
    public class HorseAllowAllRideMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Yupi.Messages.Rooms room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            uint petId = request.GetUInt32();

            RoomUser pet = room.GetRoomUserManager().GetPet(petId);

            if (pet.PetData.AnyoneCanRide == 1)
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"UPDATE pets_data SET anyone_ride = '0' WHERE id={num} LIMIT 1");

                pet.PetData.AnyoneCanRide = 0;
            }
            else
            {
                using (IQueryAdapter queryreactor2 = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryreactor2.RunFastQuery($"UPDATE pets_data SET anyone_ride = '1' WHERE id = {num} LIMIT 1");

                pet.PetData.AnyoneCanRide = 1;
            }

            router.GetComposer<PetInfoMessageComposer> ().Compose (room, pet.PetData);
            */
            throw new NotImplementedException();
        }
    }
}