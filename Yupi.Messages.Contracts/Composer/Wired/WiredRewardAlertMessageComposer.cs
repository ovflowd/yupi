using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class WiredRewardAlertMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int status)
		{
		 // Do nothing by default.
		}
	}
}
