using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class EffectsInventoryMessageComposer : AbstractComposer<IList<AvatarEffect>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<AvatarEffect> effects)
		{
		 // Do nothing by default.
		}
	}
}
