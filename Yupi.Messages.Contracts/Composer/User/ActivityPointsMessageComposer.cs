using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ActivityPointsMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int duckets, int diamonds)
		{
		 // Do nothing by default.
		}
	}
}
