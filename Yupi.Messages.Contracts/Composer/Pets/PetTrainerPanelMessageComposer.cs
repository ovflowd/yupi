using Yupi.Protocol.Buffers;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class PetTrainerPanelMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint petId)
		{
		 // Do nothing by default.
		}
	}
}
