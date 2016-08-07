using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RespectPetMessageComposer : AbstractComposer<int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int entityId)
		{
		 // Do nothing by default.
		}
	}
}
