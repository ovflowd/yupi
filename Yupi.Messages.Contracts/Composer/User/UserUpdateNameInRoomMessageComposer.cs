using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Contracts
{
	public abstract class UserUpdateNameInRoomMessageComposer : AbstractComposer<Habbo>
	{
		public override void Compose(Yupi.Protocol.ISender room, Habbo habbo)
		{
		 // Do nothing by default.
		}
	}
}
