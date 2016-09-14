using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
	public abstract class RetrieveSongIDMessageComposer : AbstractComposer<string, int>
	{
		public override void Compose(Yupi.Protocol.ISender session, string name, int songId)
		{
		 // Do nothing by default.
		}
	}
}
