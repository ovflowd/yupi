using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class PetTrainerPanelMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int petId)
		{
		 // Do nothing by default.
		}
	}
}
