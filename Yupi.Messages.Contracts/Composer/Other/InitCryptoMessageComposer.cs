using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class InitCryptoMessageComposer : AbstractComposerVoid
	{
		public override void Compose(Yupi.Protocol.ISender session)
		{
		 // Do nothing by default.
		}
	}
}
