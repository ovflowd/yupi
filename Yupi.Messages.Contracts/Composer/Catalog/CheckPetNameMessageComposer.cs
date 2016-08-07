using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class CheckPetNameMessageComposer : AbstractComposer<int, string>
	{
		public override void Compose(Yupi.Protocol.ISender session, int status, string name)
		{
		 // Do nothing by default.
		}
	}
}
