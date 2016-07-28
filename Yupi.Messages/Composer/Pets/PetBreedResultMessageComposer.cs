using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Pets
{
	public class PetBreedResultMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, int petId, int randomValue)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (petId);
				message.AppendInteger (randomValue);
				session.Send (message);
			}
		}
	}
}

