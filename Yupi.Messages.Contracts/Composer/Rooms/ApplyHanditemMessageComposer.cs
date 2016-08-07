using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ApplyHanditemMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int virtualId, int itemId)
		{
		 // Do nothing by default.
		}
	}
}
