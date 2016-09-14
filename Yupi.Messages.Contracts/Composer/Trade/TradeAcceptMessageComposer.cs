using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class TradeAcceptMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint userId, bool accepted)
		{
		 // Do nothing by default.
		}
	}
}
