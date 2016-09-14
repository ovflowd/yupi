using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class UniqueMachineIDMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string machineId)
		{
		 // Do nothing by default.
		}
	}
}
