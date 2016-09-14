using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class DoorbellMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string username)
		{
		 // Do nothing by default.
		}
	}
}
