using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class GroupAreYouSureMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int userId)
		{
		 // Do nothing by default.
		}
	}
}
