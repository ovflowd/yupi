using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class CameraStorageUrlMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string url)
		{
		 // Do nothing by default.
		}
	}
}
