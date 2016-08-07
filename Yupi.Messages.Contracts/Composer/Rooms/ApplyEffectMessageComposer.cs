using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class ApplyEffectMessageComposer : AbstractComposer<int, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, int entityId, int effectId)
		{
		 // Do nothing by default.
		}
	}
}
