using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class UpdateUsernameMessageComposer : AbstractComposer<string>
	{
		public override void Compose(Yupi.Protocol.ISender session, string newName)
		{
		 // Do nothing by default.
		}
	}
}
