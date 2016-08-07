using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class FavouriteRoomsUpdateMessageComposer : AbstractComposer<uint, bool>
	{
		public override void Compose(Yupi.Protocol.ISender session, uint roomId, bool isAdded)
		{
		 // Do nothing by default.
		}
	}
}
