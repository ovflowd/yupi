using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class BuildersClubUpdateFurniCountMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int itemsUsed)
		{
		 // Do nothing by default.
		}
	}
}
