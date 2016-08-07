using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ActivityPointsMessageComposer : AbstractComposer<uint, uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint duckets, uint diamonds)
		{
		 // Do nothing by default.
		}
	}
}
