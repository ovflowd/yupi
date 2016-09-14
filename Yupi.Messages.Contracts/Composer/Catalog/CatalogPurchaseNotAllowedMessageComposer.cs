using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class CatalogPurchaseNotAllowedMessageComposer : AbstractComposer<bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, bool isForbidden)
		{
		 // Do nothing by default.
		}
	}
}
