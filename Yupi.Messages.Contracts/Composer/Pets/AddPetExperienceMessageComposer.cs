using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class AddPetExperienceMessageComposer : AbstractComposer<PetEntity, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, PetEntity pet, int amount)
		{
		 // Do nothing by default.
		}
	}
}
