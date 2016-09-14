using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class SubscriptionStatusMessageComposer : AbstractComposer<Subscription>
	{
		public override void Compose(Yupi.Protocol.ISender session, Subscription subscription)
		{
		 // Do nothing by default.
		}
	}
}
