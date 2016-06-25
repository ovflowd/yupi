﻿using System;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;

namespace Yupi.Messages.Pets
{
	public class HorseAllowAllRideMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			Room room = Yupi.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

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
		}
	}
}
