﻿using System;

using Yupi.Protocol.Buffers;
using System.Collections.Generic;


namespace Yupi.Messages.Pets
{
	public class SellablePetBreedsMessageComposer : Yupi.Messages.Contracts.SellablePetBreedsMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, string type)
		{
			// TODO Refactor?

			/*
			string petType = PetTypeManager.GetPetTypeByHabboPetType(type);

			uint petId = PetTypeManager.GetPetRaceByItemName(petType);

			List<PetRace> races = PetTypeManager.GetRacesByPetType(petType);

			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(type);
				message.AppendInteger(races.Count);

				foreach (PetRace current in races)
				{
					message.AppendInteger(petId);
					message.AppendInteger(current.ColorOne);
					message.AppendInteger(current.ColorTwo);
					message.AppendBool(current.HasColorOne);
					message.AppendBool(current.HasColorTwo);
				}
				session.Send (message);
			}
			*/
			throw new NotImplementedException ();
		}
	}
}
