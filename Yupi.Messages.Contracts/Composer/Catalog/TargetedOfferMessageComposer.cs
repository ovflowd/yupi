using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;

namespace Yupi.Messages.Contracts
{
	public abstract class TargetedOfferMessageComposer : AbstractComposer<IList<TargetedOffer>>
	{
		public override void Compose(Yupi.Protocol.ISender session, IList<TargetedOffer> offer)
		{
		 // Do nothing by default.
		}
	}
}
