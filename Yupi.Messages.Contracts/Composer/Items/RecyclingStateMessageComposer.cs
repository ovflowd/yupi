using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RecyclingStateMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int insertId)
		{
		 // Do nothing by default.
		}
	}
}
