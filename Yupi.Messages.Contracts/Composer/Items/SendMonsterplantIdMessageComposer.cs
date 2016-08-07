using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class SendMonsterplantIdMessageComposer : AbstractComposer<uint>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint entityId)
		{
		 // Do nothing by default.
		}
	}
}
