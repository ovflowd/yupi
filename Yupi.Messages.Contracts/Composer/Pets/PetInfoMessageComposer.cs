using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class PetInfoMessageComposer : AbstractComposer<PetInfo>
	{
		public override void Compose(Yupi.Protocol.ISender room, PetInfo pet)
		{
		 // Do nothing by default.
		}
	}
}
