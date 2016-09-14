using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FavouriteGroupMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int userId)
		{
		 // Do nothing by default.
		}
	}
}
