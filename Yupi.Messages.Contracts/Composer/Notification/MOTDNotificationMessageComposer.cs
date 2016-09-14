using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class MOTDNotificationMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string text)
		{
		 // Do nothing by default.
		}
	}
}
