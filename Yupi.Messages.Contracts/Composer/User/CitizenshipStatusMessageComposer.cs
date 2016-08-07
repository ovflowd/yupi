using Yupi.Protocol.Buffers;
using Yupi.Net;

namespace Yupi.Messages.Contracts
{
	public abstract class CitizenshipStatusMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string value)
		{
		 // Do nothing by default.
		}
	}
}
