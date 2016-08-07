using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FloodFilterMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int remainingSeconds)
		{
		 // Do nothing by default.
		}
	}
}
