using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
	public abstract class LoveLockDialogueMessageComposer : AbstractComposer< Yupi.Protocol.ISender, LovelockItem>
	{
		public override void Compose(Yupi.Protocol.ISender user1,  Yupi.Protocol.ISender user2, LovelockItem loveLock)
		{
		 // Do nothing by default.
		}
	}
}
