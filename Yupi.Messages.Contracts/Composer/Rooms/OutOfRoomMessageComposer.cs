using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class OutOfRoomMessageComposer : AbstractComposer<short>
	{
		public override void Compose(Yupi.Protocol.ISender session, short code = 0)
		{
		 // Do nothing by default.
		}
	}
}
