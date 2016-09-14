using Yupi.Model.Domain;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class PetRespectNotificationMessageComposer : AbstractComposer<PetEntity>
	{
		public override void Compose(Yupi.Protocol.ISender session, PetEntity pet)
		{
		 // Do nothing by default.
		}
	}
}
