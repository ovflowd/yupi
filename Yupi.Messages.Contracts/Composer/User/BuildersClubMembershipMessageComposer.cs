using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class BuildersClubMembershipMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int expire, int maxItems)
		{
		 // Do nothing by default.
		}
	}
}
