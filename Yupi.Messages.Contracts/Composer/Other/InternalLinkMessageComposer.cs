using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class InternalLinkMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string link)
		{
		 // Do nothing by default.
		}
	}
}
