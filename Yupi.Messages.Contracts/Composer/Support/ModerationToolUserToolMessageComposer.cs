using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ModerationToolUserToolMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint userId)
		{
		 // Do nothing by default.
		}
	}
}
