using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class TargetedOfferMessageComposer : AbstractComposer<TargetedOffer>
	{
		public override void Compose(Yupi.Protocol.ISender session, TargetedOffer offer)
		{
		 // Do nothing by default.
		}
	}
}
