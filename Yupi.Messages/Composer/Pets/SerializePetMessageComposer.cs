using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Pets
{
	public class SerializePetMessageComposer : Yupi.Messages.Contracts.SerializePetMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, PetEntity pet)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(pet.Id);
				message.AppendInteger(pet.Info.Id);
				message.AppendInteger(pet.Info.RaceId);
				message.AppendInteger(pet.Info.Race);
				message.AppendString(pet.Info.Color.ToLower());
				if (pet.Info.HaveSaddle)
				{
					message.AppendInteger(2);
					message.AppendInteger(3);
					message.AppendInteger(4);
					message.AppendInteger(9);
					message.AppendInteger(0);
					message.AppendInteger(3);
					message.AppendInteger(pet.Info.Hair);
					message.AppendInteger(pet.Info.HairDye);
					message.AppendInteger(3);
					message.AppendInteger(pet.Info.Hair);
					message.AppendInteger(pet.Info.HairDye);
				}
				else
				{
					message.AppendInteger(1);
					message.AppendInteger(2);
					message.AppendInteger(2);
					message.AppendInteger(pet.Info.Hair);
					message.AppendInteger(pet.Info.HairDye);
					message.AppendInteger(3);
					message.AppendInteger(pet.Info.Hair);
					message.AppendInteger(pet.Info.HairDye);
				}
				message.AppendBool(pet.Info.HaveSaddle);
				throw new NotImplementedException ();
				//message.AppendBool(pet.RidingHorse);
				room.Send (message);
			}
		}
	}
}

