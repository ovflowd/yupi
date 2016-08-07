using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class TradeStartMessageComposer : AbstractComposer<uint, uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint firstUserId, uint secondUserId)
		{
		 // Do nothing by default.
		}
	}
}
