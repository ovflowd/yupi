using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class MinimailCountMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int count)
		{
		 // Do nothing by default.
		}
	}
}
